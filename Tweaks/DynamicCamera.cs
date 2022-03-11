using Terraria;
namespace LittleTweaks.Tweaks
{
    static class DynamicCamera
    {
        public static bool Enabled { get; private set; }
        public static int Value { get; set; }

        public static void Enable(bool state)
        {
            if (state == Enabled)
                return;

            Enabled = state;
            if (state)
            {
                On.Terraria.Main.ClampScreenPositionToWorld += Main_ClampScreenPositionToWorld;
            }
            else
            {
                On.Terraria.Main.ClampScreenPositionToWorld -= Main_ClampScreenPositionToWorld;
            }
        }

        private static void Main_ClampScreenPositionToWorld(On.Terraria.Main.orig_ClampScreenPositionToWorld orig)
        {
            if (Enabled && Value > 0) 
            {
                Main.screenPosition.X = (int)(Main.screenPosition.X + (Main.mouseX - (Main.screenWidth / 2)) / Value);
                Main.screenPosition.Y = (int)(Main.screenPosition.Y + (Main.mouseY - (Main.screenHeight / 2)) / Value);
            }
            orig();
            
        }
    }
}
