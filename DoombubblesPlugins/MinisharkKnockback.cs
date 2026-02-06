using PluginLoader;
using Terraria;
using Terraria.ID;

namespace DoombubblesTerrariaPlugins
{
    public class MinisharkKnockback : DoombubblesPlugin, IPluginItemSetDefaults
    {
        public void OnItemSetDefaults(Item item)
        {
            if (item.type != ItemID.Minishark) return;

            item.knockBack = 1;
        }
    }
}