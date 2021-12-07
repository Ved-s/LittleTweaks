using Terraria.ModLoader.Config;

namespace LittleTweaks
{
    class TweaksConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Tooltip("Enable/disable Ctrl and Shift inventory behavior from 1.4")]
        public bool CtrlShiftTweakEnabled { get => Tweaks.CtrlShift.Enabled;     set => Tweaks.CtrlShift.Enable(value); }

        [Tooltip("Enable/disable \"always-available\" ruler line from 1.4")]
        public bool RulerAlwaysEnabled    { get => Tweaks.Ruler.Enabled;         set => Tweaks.Ruler.Enable(value);     }

        [Tooltip("Enable/disable dynamic camera offset (moving cursor affects camera position)")]
        public bool DynamicCameraEnabled  { get => Tweaks.DynamicCamera.Enabled; set => Tweaks.DynamicCamera.Enable(value); }

        [Tooltip("Enable/disable shift-right-click on item to take half amount")]
        public bool HalfStackEnabled { get => Tweaks.HalfStack.Enabled; set => Tweaks.HalfStack.Enable(value); }

        [Range(1,80)]
        [Tooltip("Defines how much camera will be offsetted, works a bit different with Smooth Camera from OriMod")]
        public int DynamicCameraValue     { get => Tweaks.DynamicCamera.Value;   set => Tweaks.DynamicCamera.Value = value; }

        internal static TweaksConfig Instance;

        public TweaksConfig() 
        {
            Instance = this;
            CtrlShiftTweakEnabled = true;
            RulerAlwaysEnabled = true;
            DynamicCameraEnabled = false;
            HalfStackEnabled = true;
            DynamicCameraValue = 4;
        }

    }
}
