using PluginLoader;
using Terraria;
using Terraria.ID;

namespace DoombubblesTerrariaPlugins
{
    public class MinionIFrames : DoombubblesPlugin, IPluginProjectileAI
    {
        public void OnProjectileAI001(Projectile projectile)
        {
            switch (projectile.type)
            {
                case ProjectileID.ImpFireball:
                case ProjectileID.VampireFrog:
                case ProjectileID.BabySlime:
                case ProjectileID.VenomSpider:
                case ProjectileID.JumperSpider:
                case ProjectileID.DangerousSpider:
                case ProjectileID.Retanimini:
                case ProjectileID.Spazmamini:
                case ProjectileID.Tempest:
                    StaticToLocal(projectile);
                    break;
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