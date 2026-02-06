using System.Linq;
using DoombubblesTerrariaPlugins;
using PluginLoader;
using Terraria;

// ReSharper disable once CheckNamespace
namespace DoombubblesPlugins
{
    public class PermaAmmo : DoombubblesPlugin, IPluginPlayerPickAmmo, IPluginPlayerUpdate
    {
        private static readonly Setting<int> RequiredCount = 9999;

        public void OnPlayerPickAmmo(Player player, Item weapon, ref int shoot, ref float speed, ref bool canShoot,
            ref int damage, ref float knockback, ref int usedAmmoItemId, bool dontConsume)
        {
            foreach (var item in player.inventory.Where(item =>
                         item.active && weapon.useAmmo == item.ammo && item.stack == RequiredCount - 1))
            {
                item.stack = RequiredCount;
            }
        }


        public void OnPlayerUpdate(Player player)
        {
            if (player.HeldItem != null && player.HeldItem.active && player.itemTime == 1 &&
                player.HeldItem.damage > 0 && player.HeldItem.consumable && player.HeldItem.stack == RequiredCount - 1)
            {
                player.HeldItem.stack = RequiredCount;
            }
        }
    }
}