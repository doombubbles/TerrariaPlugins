using Terraria.GameContent.Prefixes;
using Terraria.ID;

namespace DoombubblesTerrariaPlugins
{
    public class LegendaryYoYos : DoombubblesPlugin
    {
        public override void OnInitialize()
        {
            for (var i = 0; i < ItemID.Sets.Yoyo.Length; i++)
            {
                if (!ItemID.Sets.Yoyo[i]) continue;

                PrefixLegacy.ItemSets.BoomerangsChakrams[i] = false;
                PrefixLegacy.ItemSets.ItemsThatCanHaveLegendary2[i] = true;
            }
        }
    }
}