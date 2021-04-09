using Terraria;
using Terraria.ModLoader;

namespace LittleTweaks
{
    class TweakGlobalPlayer : ModPlayer
    {
        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
        {
            TweaksConfig config = ModContent.GetInstance<TweaksConfig>();

            if (config.RulerAlwaysEnabled) player.rulerLine = true;
        }
    }
}
