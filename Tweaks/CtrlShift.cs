using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.ID;

namespace LittleTweaks.Tweaks
{
    static class CtrlShift
    {
        
        public static bool Enabled { get; private set; }

        public static void Enable(bool state) 
        {
			Enabled = state;
            if (state) 
            {
                On.Terraria.UI.ItemSlot.LeftClick_ItemArray_int_int += ItemSlot_LeftClick;
                On.Terraria.UI.ItemSlot.OverrideHover += ItemSlot_OverrideHover;
                On.Terraria.UI.ItemSlot.SellOrTrash += ItemSlot_SellOrTrash;
            }
			else 
			{
				On.Terraria.UI.ItemSlot.LeftClick_ItemArray_int_int -= ItemSlot_LeftClick;
				On.Terraria.UI.ItemSlot.OverrideHover -= ItemSlot_OverrideHover;
				On.Terraria.UI.ItemSlot.SellOrTrash -= ItemSlot_SellOrTrash;
			}
        }

        private static void ItemSlot_SellOrTrash(On.Terraria.UI.ItemSlot.orig_SellOrTrash orig, Terraria.Item[] inv, int context, int slot)
		{
			return;
		}

		private static void ItemSlot_OverrideHover(On.Terraria.UI.ItemSlot.orig_OverrideHover orig, Terraria.Item[] inv, int context, int slot)
        {
			Item item = inv[slot];
			if (ItemSlot.ShiftInUse && item.type > 0 && item.stack > 0 && !inv[slot].favorited)
			{
				switch (context)
				{
					case 0:
					case 1:
					case 2:
						if (Main.player[Main.myPlayer].chest != -1)
						{
							if (ChestUI.TryPlacingInChest(item, true))
							{
								Main.cursorOverride = 9;
							}
						}
						break;
					case 3:
					case 4:
						if (Main.player[Main.myPlayer].ItemSpace(item))
						{
							Main.cursorOverride = 8;
						}
						break;
					case 5:
					case 8:
					case 9:
					case 10:
					case 11:
					case 12:
					case 16:
					case 17:
					case 18:
					case 19:
					case 20:
						if (Main.player[Main.myPlayer].ItemSpace(inv[slot]))
						{
							Main.cursorOverride = 7;
						}
						break;
				}
			}
			if (IsCtrlPressed() && item.type > 0 && item.stack > 0 && !inv[slot].favorited) 
			{
				if (context < 3) 
				{
					if (Main.npcShop > 0 && !item.favorited)
					{
						Main.cursorOverride = 10;
					}
					else
					{
						Main.cursorOverride = 6;
					}
				}
			}
			if (Main.keyState.IsKeyDown(Main.FavoriteKey) && context < 3)
			{
				if (item.type > 0 && item.stack > 0 && Main.drawingPlayerChat)
				{
					Main.cursorOverride = 2;
					return;
				}
				if (item.type > 0 && item.stack > 0)
				{
					Main.cursorOverride = 3;
				}
			}
		}

        private static void ItemSlot_LeftClick(On.Terraria.UI.ItemSlot.orig_LeftClick_ItemArray_int_int orig, Terraria.Item[] inv, int context, int slot)
        {
			if (IsCtrlPressed()) 
			{
				SellOrTrash(inv, context, slot);
				return;
			}
			orig(inv, context, slot);
        }

		public static void SellOrTrash(Item[] inv, int context, int slot)
		{
			Player player = Main.player[Main.myPlayer];
			if (inv[slot].type <= 0)
			{
				return;
			}
			if (Main.npcShop > 0 && !inv[slot].favorited)
			{
				Chest chest = Main.instance.shop[Main.npcShop];
				if (inv[slot].type < ItemID.CopperCoin || (inv[slot].type > ItemID.PlatinumCoin && PlayerHooks.CanSellItem(player, Main.npc[player.talkNPC], chest.item, inv[slot])))
				{
					if (player.SellItem(inv[slot].value, inv[slot].stack))
					{
						int num = chest.AddShop(inv[slot]);
						inv[slot].SetDefaults(0, false);
						Main.PlaySound(18, -1, -1, 1, 1f, 0f);
						Recipe.FindRecipes();
						PlayerHooks.PostSellItem(player, Main.npc[player.talkNPC], chest.item, chest.item[num]);
						return;
					}
					if (inv[slot].value == 0)
					{
						int num2 = chest.AddShop(inv[slot]);
						inv[slot].SetDefaults(0, false);
						Main.PlaySound(7, -1, -1, 1, 1f, 0f);
						Recipe.FindRecipes();
						PlayerHooks.PostSellItem(player, Main.npc[player.talkNPC], chest.item, chest.item[num2]);
						return;
					}
				}
			}
			else if (!inv[slot].favorited && !ItemSlot.Options.DisableLeftShiftTrashCan)
			{
				Main.PlaySound(7, -1, -1, 1, 1f, 0f);
				player.trashItem = inv[slot].Clone();
				inv[slot].SetDefaults(0, false);
				if (context == 3 && Main.netMode == NetmodeID.MultiplayerClient)
				{
					NetMessage.SendData(MessageID.SyncChestItem, -1, -1, null, player.chest, (float)slot, 0f, 0f, 0, 0, 0);
				}
				Recipe.FindRecipes();
			}
		}

		static bool IsCtrlPressed() 
		{
			KeyboardState ks = Keyboard.GetState();
			return ks.IsKeyDown(Keys.LeftControl) || ks.IsKeyDown(Keys.RightControl);
		}
	}
}
