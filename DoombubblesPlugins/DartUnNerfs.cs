using PluginLoader;
using Terraria;
using Terraria.ID;

namespace DoombubblesTerrariaPlugins
{
    public class DartUnNerfs : DoombubblesPlugin, IPluginProjectileAI, IPluginItemSetDefaults
    {
        private static readonly Setting<int> CrystalDartPenetrate = 7;
        private static readonly Setting<bool> CrystalDamageDropoff = false;
        private static readonly Setting<int> IchorDartDamage = 10;
        private static readonly Setting<int> CursedDartPenetrate = 1;
        private static readonly Setting<int> CursedDartFlamePenetrate = 3;

        public void OnItemSetDefaults(Item item)
        {
            if (item.type == ItemID.IchorDart)
            {
                item.damage = IchorDartDamage;
            }
        }

        public void OnProjectileAI001(Projectile projectile)
        {
            switch (projectile.type)
            {
                case ProjectileID.CrystalDart:
                {
                    if (projectile.originalDamage <= 0)
                    {
                        projectile.originalDamage = projectile.damage;
                        projectile.penetrate = CrystalDartPenetrate;
                    }
                    else if (!CrystalDamageDropoff)
                    {
                        projectile.damage = projectile.originalDamage;
                    }

                    break;
                }
                case ProjectileID.CursedDart:
                {
                    if (projectile.originalDamage <= 0)
                    {
                        projectile.originalDamage = projectile.damage;
                        projectile.penetrate = CursedDartPenetrate;
                    }

                    break;
                }
                case ProjectileID.CursedDartFlame:
                {
                    if (projectile.originalDamage <= 0)
                    {
                        projectile.originalDamage = projectile.damage;
                        projectile.penetrate = CursedDartFlamePenetrate;
                    }

                    break;
                }
            }
        }
    }
}