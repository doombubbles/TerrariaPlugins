using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using PluginLoader;
using Terraria;

namespace DoombubblesTerrariaPlugins
{
    public class DoombubblesPlugin : MarshalByRefObject, IPluginInitialize
    {
        public static readonly Dictionary<string, DoombubblesPlugin> Plugins =
            new Dictionary<string, DoombubblesPlugin>();

        public readonly Dictionary<string, Setting> Settings = new Dictionary<string, Setting>();

        public string Name
        {
            get { return GetType().Name; }
        }

        private static bool initialized;

        public static DateTimeOffset IgnoreUpdatesUntil = DateTimeOffset.MinValue;



        protected static Player Player
        {
            get { return Main.LocalPlayer; }
        }

        /// <summary>
        /// Watch for changes to Plugins.ini (but use <see cref="IgnoreUpdatesUntil"/> to ignore changes from code)
        /// </summary>
        static DoombubblesPlugin()
        {
            var fileSystemWatcher = new FileSystemWatcher(Environment.CurrentDirectory, "Plugins.ini");
            fileSystemWatcher.Changed += (sender, args) =>
            {
                if (!initialized || IgnoreUpdatesUntil > DateTimeOffset.Now) return;

                IgnoreUpdatesUntil = DateTimeOffset.Now.AddMilliseconds(100);
                Main.NewText("Updated DoombubblesPlugin settings from Plugins.ini");
                foreach (var setting in Plugins.Values.SelectMany(plugin => plugin.Settings.Values))
                {
                    setting.Load(true);
                }
            };
            fileSystemWatcher.EnableRaisingEvents = true;

            // Some pages that are helpful for editing Plugins.ini
            const string helpfulLinks = "HelpfulLinks";
            IniAPI.WriteIni(helpfulLinks, "DoombubblesPluginReadme",
                "https://github.com/doombubbles/DoombubblesTerrariaPlugins/blob/main/README.md");
            IniAPI.WriteIni(helpfulLinks, "XnaHotkeyNames",
                "https://docs.monogame.net/api/Microsoft.Xna.Framework.Input.Keys.html");
            IniAPI.WriteIni(helpfulLinks, "TerrariaInventorySlots",
                "https://tshock.readme.io/docs/slot-indexes");
        }

        /// <summary>
        /// Automatically add settings from fields via reflection
        /// </summary>
        public DoombubblesPlugin()
        {
            Plugins.Add(Name, this);

            foreach (var info in GetType().GetFields(BindingFlags.Static | BindingFlags.Instance |
                                                     BindingFlags.Public | BindingFlags.NonPublic)
                         .Where(field => typeof(Setting).IsAssignableFrom(field.FieldType)))
            {
                var setting = (Setting) info.GetValue(this);
                AddSetting(info.Name, setting);
            }
        }

        /// <summary>
        /// Register a setting to this plugin
        /// </summary>
        protected void AddSetting(string name, Setting setting)
        {
            setting.section = Name;
            setting.name = name;
            setting.Register();

            Settings.Add(setting.name, setting);
        }

        /// <summary>
        /// Register a hotkey setting to this plugin
        /// </summary>
        protected void AddHotkeySetting(string name, HotkeySetting setting)
        {
            AddSetting(name, setting);
        }

        public void OnInitialize()
        {
            initialized = true;
        }
    }


    public abstract class Setting
    {
        protected object value;

        public string section;
        public string name;

        public abstract override string ToString();
        public abstract void SetFrom(string val);

        public virtual void Register()
        {
            Load();
        }

        public void Load(bool printChanges = false)
        {
            try
            {
                var before = ToString();
                SetFrom(IniAPI.ReadIni(section, name, ToString(), writeIt: true));
                if (printChanges && before != ToString())
                {
                    Main.NewText(string.Format("{0}.{1} changed", section, name));
                }
            }
            catch (Exception e)
            {
                Main.NewText(string.Format("Failed to load {0}.{1}: {2}", section, name, e.Message));
            }
        }

        public void Save()
        {
            try
            {
                DoombubblesPlugin.IgnoreUpdatesUntil = DateTimeOffset.Now.AddMilliseconds(100);
                IniAPI.WriteIni(section, name, ToString());
            }
            catch (Exception e)
            {
                Main.NewText(string.Format("Failed to save {0}.{1}: {2}", section, name, e.Message));
            }
        }
    }

    public class Setting<T> : Setting
    {
        public T Value
        {
            get { return (T) value; }
            set
            {
                this.value = value;
                Save();
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(value);
        }

        public override void SetFrom(string val)
        {
            value = JsonConvert.DeserializeObject<T>(val);
        }

        public static implicit operator T(Setting<T> thisGuy)
        {
            return thisGuy.Value;
        }

        public static implicit operator Setting<T>(T value)
        {
            return new Setting<T> { value = value };
        }
    }

    public class HotkeySetting : Setting<Hotkey>
    {
        public override string ToString()
        {
            return value.ToString();
        }

        public override void SetFrom(string val)
        {
            var hotkey = Loader.ParseHotkey(val);

            if (hotkey == null)
            {
                Value.Key = Keys.None;
                Value.Alt = false;
                Value.Shift = false;
                Value.Control = false;
                Value.IgnoreModifierKeys = false;
                return;
            }

            Value.Key = hotkey.Key;
            Value.Alt = hotkey.Alt;
            Value.Shift = hotkey.Shift;
            Value.Control = hotkey.Control;
            Value.IgnoreModifierKeys = hotkey.IgnoreModifierKeys;
        }

        public override void Register()
        {
            base.Register();
            Loader.RegisterHotkey(Value);
        }

        public static implicit operator HotkeySetting(Keys value)
        {
            return new HotkeySetting { value = new Hotkey { Key = value } };
        }

        public static implicit operator HotkeySetting(Hotkey hotkey)
        {
            return new HotkeySetting { value = hotkey };
        }
    }


    public class SettingCommand : MarshalByRefObject, IPluginChatCommand
    {
        public virtual bool OnChatCommand(string command, string[] args)
        {
            if (command != "setting") return false;

            if (args.Length == 0)
            {
                Main.NewText(string.Join("\n",
                    "Usage:",
                    "   /setting - Shows this information about Settings",
                    "   /setting [PluginName] - Lists all Settings for the given Plugin",
                    "   /setting [PluginName] [SettingName] - Displays the value of a Setting for a Plugin",
                    "   /setting [PluginName] [SettingName] [value] - Sets the value of a Setting for a Plugin",
                    "The following plugins have settings: " +
                    string.Join(", ", DoombubblesPlugin.Plugins.Values.Where(p => p.Settings.Any()).Select(p => p.Name))
                ));
                return true;
            }

            if (!DoombubblesPlugin.Plugins.ContainsKey(args[0]))
            {
                Main.NewText("Plugin " + args[0] +
                             " does not exist or isn't compatible with the settings command");
                return true;
            }

            var plugin = DoombubblesPlugin.Plugins[args[0]];
            if (!plugin.Settings.Any())
            {
                Main.NewText("Plugin " + args[0] + " does not have any settings");
                return true;
            }

            if (args.Length == 1)
            {
                Main.NewText(string.Join("\n", plugin.Settings.Keys.Select(s => "    " + s)
                    .Prepend("Plugin " + args[0] + " has the following settings:")));
                return true;
            }

            if (!plugin.Settings.ContainsKey(args[1]))
            {
                Main.NewText("Plugin " + args[1] + " does not have a setting named " + args[1]);
                return true;
            }

            var setting = plugin.Settings[args[1]];

            if (args.Length > 2)
            {
                var value = string.Join(" ", args.Skip(2));

                try
                {
                    setting.SetFrom(value);
                }
                catch (Exception e)
                {
                    Main.NewText("Failed to set value: " + e.Message);
                    return true;
                }

                try
                {
                    setting.Save();
                }
                catch (Exception e)
                {
                    Main.NewText("Failed to save value: " + e.Message);
                    return true;
                }
            }

            Main.NewText("[" + plugin.Name + "]\n" + setting.name + "=" + setting);
            return true;
        }
    }
}