using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace LittleTweaks
{
    static class HotKeys
    {
        public static Dictionary<ModHotKey, Action<Player>> Hotkeys;

        public static void Init() 
        {
            Hotkeys = new Dictionary<ModHotKey, Action<Player>>();
        }

        public static void CheckTriggers(Player p) 
        {
            foreach (KeyValuePair<ModHotKey, Action<Player>> kvp in Hotkeys)
                if (kvp.Key.JustPressed) kvp.Value(p);
        }
    }
}
