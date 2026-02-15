using PluginLoader;
using Terraria;

namespace DoombubblesTerrariaPlugins
{
    public class TrashRemovesPrefix : DoombubblesPlugin, IPluginPlayerUpdate
    {
        public void OnPlayerUpdate(Player player)
        {
            if (player.trashItem != null && player.trashItem.active && player.trashItem.prefix > 0)
            {
                player.trashItem.ResetPrefix();
            }
        }
    }
}