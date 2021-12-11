using Terraria.ModLoader;

namespace LittleTweaks
{
    public class LittleTweaks : Mod
    {
        internal static LittleTweaks Instance;
        public LittleTweaks() 
        {
            Instance = this;
        }

        public override void Load()
        {
            HotKeys.Init();
            Tweaks.ChestSwitcher.Init();
        }

        public override void Unload()
        {
            TweaksConfig.Instance.Unload();
            HotKeys.Unload();
        }
    }
    
}