using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace LittleTweaks
{
    class TweakGlobalPlayer : ModPlayer
    {
        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
        {
            Tweaks.Ruler.Tweak(player);
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            HotKeys.CheckTriggers(player);
        }
    }
}
