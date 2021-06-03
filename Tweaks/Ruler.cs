using Terraria;

namespace LittleTweaks.Tweaks
{
    static class Ruler
    {
        public static bool Enabled { get; private set; }

        public static void Enable(bool state) 
        {
            Enabled = state;
        }

        public static void Tweak(Player player) 
        {
            if (Enabled) player.rulerLine = true;
        }
    }
}
