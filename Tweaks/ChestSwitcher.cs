using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace LittleTweaks.Tweaks
{
    static class ChestSwitcher
    {
        public static void Init() 
        {
            HotKeys.Hotkeys.Add(LittleTweaks.Instance.RegisterHotKey("Switch Chest Left", "OemComma"), (p) => SwitchChest(p,false));
            HotKeys.Hotkeys.Add(LittleTweaks.Instance.RegisterHotKey("Switch Chest Right", "OemPoint"), (p) => SwitchChest(p, true));
        }

        public static void SwitchChest(Player p, bool dirRight)
        {
            if (p.chest == -1) return;
            int dir = (dirRight ? 2 : -2);

            int newChest = TryFindChest(p.chestX + dir, p.chestY);
            if (newChest == -1) return;
            p.chest = newChest;
            p.chestX += dir;
        }
        static int TryFindChest(int x, int y) 
        {
            for (int i = 0; i < Main.chest.Length; i++) 
            {
                Chest c = Main.chest[i];
                if (c is null) continue;
                if (x == c.x && y == c.y) return i;
            }
            return -1;
        }
    }
}
