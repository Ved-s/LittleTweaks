using Terraria.ModLoader.Config;

namespace LittleTweaks
{
    class TweaksConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public bool CtrlShiftTweakEnabled { get => CtrlShiftTweak.Enabled; set => CtrlShiftTweak.Enable(value); }
        public bool RulerAlwaysEnabled;

        internal static TweaksConfig Instance;

        public TweaksConfig() 
        {
            Instance = this;
            CtrlShiftTweakEnabled = true;
            RulerAlwaysEnabled = true;
        }

    }
}
