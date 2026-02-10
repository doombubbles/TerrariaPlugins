using PluginLoader;
using Terraria;
using Terraria.ID;

namespace DoombubblesTerrariaPlugins
{
    public class GunsMinimumKnockback : DoombubblesPlugin, IPluginItemSetDefaults
    {
        public void OnItemSetDefaults(Item item)
        {
            if (item.useAmmo == AmmoID.Bullet && item.knockBack == 0)
            {
                item.knockBack = 1;
            }
        }
    }
}