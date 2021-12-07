using System;
using Terraria;
using Terraria.ID;

namespace LittleTweaks.Tweaks
{
    static class HalfStack
    {
        public static bool Enabled { get; private set; }

        public static void Enable(bool state)
        {
            Enabled = state;
            if (state)
            {
                On.Terraria.UI.ItemSlot.RightClick_ItemArray_int_int += ItemSlot_RightClick_ItemArray_int_int;
            }
            else
            {
                On.Terraria.UI.ItemSlot.RightClick_ItemArray_int_int -= ItemSlot_RightClick_ItemArray_int_int;
            }
        }

        private static void ItemSlot_RightClick_ItemArray_int_int(On.Terraria.UI.ItemSlot.orig_RightClick_ItemArray_int_int orig, Terraria.Item[] inv, int context, int slot)
        {
            if (Main.stackSplit == 0 && Main.mouseRight && Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift))
            {
                Item item = inv[slot];
                if (item == null || item.IsAir) return;

                if (!(Main.mouseItem.IsAir || item.IsTheSameAs(Main.mouseItem))) 
                {
                    orig(inv, context, slot);
                    return;
                }
                if (Main.mouseItem.IsAir) 
                {
                    Main.mouseItem = item.Clone();
                    Main.mouseItem.stack = 0;
                }
                int amount = Math.Min(item.stack / 2, item.maxStack - Main.mouseItem.stack);
                Main.mouseItem.stack += amount;
                item.stack -= amount;

                if (item.stack <= 0) inv[slot] = new Item();

                Recipe.FindRecipes();
                Main.soundInstanceMenuTick.Stop();
                Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
                Main.PlaySound(SoundID.MenuTick, -1, -1, 1, 1f, 0f);
                if (Main.stackSplit == 0)
                {
                    Main.stackSplit = 15;
                }
                else
                {
                    Main.stackSplit = Main.stackDelay;
                }
                if (context == 3 && Main.netMode == NetmodeID.MultiplayerClient)
                {
                    NetMessage.SendData(MessageID.SyncChestItem, -1, -1, null, Main.player[Main.myPlayer].chest, (float)slot, 0f, 0f, 0, 0, 0);
                }
            }
            else orig(inv, context, slot);
        }
    }
}
