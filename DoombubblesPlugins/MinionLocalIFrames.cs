using System.Linq;
using PluginLoader;
using Terraria;
using Terraria.ID;

namespace DoombubblesTerrariaPlugins
{
    public class MinionLocalIFrames : DoombubblesPlugin, IPluginProjectileAI
    {
        private static readonly Setting<int[]> AffectedProjectiles = new int[]
        {
            ProjectileID.ImpFireball,
            ProjectileID.VampireFrog,
            ProjectileID.BabySlime,
            ProjectileID.VenomSpider,
            ProjectileID.JumperSpider,
            ProjectileID.DangerousSpider,
            ProjectileID.Retanimini,
            ProjectileID.Spazmamini,
            ProjectileID.Tempest,
        };

        public void OnProjectileAI001(Projectile projectile)
        {
            if (AffectedProjectiles.Value.Contains(projectile.type))
            {
                StaticToLocal(projectile);
            }
        }

        private static void StaticToLocal(Projectile projectile)
        {
            if (!projectile.usesIDStaticNPCImmunity || projectile.usesLocalNPCImmunity) return;

            projectile.localNPCHitCooldown = projectile.idStaticNPCHitCooldown;
            projectile.idStaticNPCHitCooldown = -1;
            projectile.usesIDStaticNPCImmunity = false;
            projectile.usesLocalNPCImmunity = true;
        }
    }
}