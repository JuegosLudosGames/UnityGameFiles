using JLG.gift.cSharp.ui.menu;
using JLG.gift.cSharp.background.input;
using JLG.gift.cSharp.inventory;
using JLG.gift.cSharp.entity.player.data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.ui.inventory {
	public class InventorySystem : ScreenMenu {

		[Header("ItemHolders")]
		public InventoryMenu menu;
		public InventoryHotbar hotbar;
		[Header("Selector")]
		public GameObject selector;
		[Header("Control")]
		public GameObject cursorObj;


		public bool menuActive {
			get; private set;
		}

		private Canvas canvas;
		public int selectedSlot {
			get; private set;
		}
		public int slotLength {
			get; private set;
		}

		// Start is called before the first frame update
		void Start() {
			menu.resetScroll();
			setActive(menu.gameObject, false);
			menuActive = false;
			canvas = GetComponent<Canvas>();
			updateInventory();
			selectSlot(0);
			slotLength = hotbar.length;
		}

		// Update is called once per frame
		void Update() {
			if (Input.GetKeyDown(KeyInput.InventoryToggle) && !GameObject.FindGameObjectWithTag("PauseSystem").GetComponent<pauseSystem.PauseSystem>().isPaused) {
				toggleMenu();
			}
		}

		public void selectSlot(int hotbarSlot) {
			selectedSlot = hotbarSlot;
			selector.GetComponent<RectTransform>().localPosition = hotbar.getTransform(hotbarSlot).localPosition;
		}

		public InvItem getSelectedItem() {
			return getSlotContent(selectedSlot);
		}

		public InvItem getSlotContent(int hotbarSlot) {
			if (PlayerData.instance.inventory.isItemPresent(Inventory.HotbarSlotToSlot(hotbarSlot))) {
				return PlayerData.instance.inventory.getItemInSlot(Inventory.HotbarSlotToSlot(hotbarSlot)).item;
			} else
				return null;
		}

		public InvItem consumeSlotOnce(int hotbarSlot) {
			return consumeSlot(hotbarSlot, 1);
		}

		public InvItem consumeSlot(int hotbarSlot, int times) {
			Inventory inv = PlayerData.instance.inventory;
			int mSlot = Inventory.HotbarSlotToSlot(hotbarSlot);
			ItemStack i = inv.getItemInSlot(mSlot);
			
			if (!(i is null)) {
				if (i.amount > times) {
					i.amount -= times;
					hotbar.placeItem(i, hotbarSlot);
					return i.item;
				} else if (i.amount == times) {
					inv.removeItemInSlot(mSlot);
					hotbar.emptySlot(hotbarSlot);
					return i.item;
				} else {
					return null;
				}
			} else {
				return null;
			}
		}

		public InvItem consumeSlot(int times) {
			return consumeSlot(selectedSlot, times);
		}

		public InvItem consumeSlotOnce() {
			return consumeSlot(1);
		}

		public void updateInventory() {
			Inventory inv = PlayerData.instance.inventory;
			ItemStack[] con = inv.Contents.ToArray();
			for (int x = 0; x < con.Length; x++) {
				if (con[x] != null) {
					menu.placeItem(con[x], x);
				} else {
					menu.emptySlot(x);
				}
			}

			con = inv.HotBarContents.ToArray();
			for (int x = 0; x < con.Length; x++) {
				if (con[x] != null) {
					hotbar.placeItem(con[x], x);
				} else {
					hotbar.emptySlot(x);
				}
			}

			con = inv.AssesContents.ToArray();
			for (int x = 0; x < con.Length; x++) {
				if (con[x] != null) {
					menu.placeSpecialItem(con[x], x);
				} else {
					menu.emptySpecialSlot(x);
				}
			}
		}

		public void toggleMenu() {
			//sendTo(active, menu.gameObject);
			if (menuActive) {
				menu.resetScroll();
				setActive(menu.gameObject, false);
				menuActive = false;
				Time.timeScale = 1.0f;
				hotbar.disableAllPops();
			} else {
				setActive(menu.gameObject, true);
				menuActive = true;
				updateInventory();
				Time.timeScale = 0.0f;
				hotbar.enableAllPops(PlayerData.instance.inventory);
			}
		}

		public void placeItem(int slot, ItemStack item) {
			if (slot >= Inventory.assessInd) {
				menu.placeSpecialItem(item, Inventory.slotToAssessSlot(slot));
			} else if (slot >= Inventory.hotbarInd) {
				hotbar.placeItem(item, Inventory.slotToHotbarSlot(slot));
			} else {
				menu.placeItem(item, slot);
			}
		}

		public void merge(int slot, ItemStack item) {
			if (slot >= Inventory.assessInd) {
				menu.mergeSpecialItem(item, Inventory.slotToAssessSlot(slot));
			} else if (slot >= Inventory.hotbarInd) {
				hotbar.mergeItem(item, Inventory.slotToHotbarSlot(slot));
			} else {
				menu.mergeItem(item, slot);
			}
		}

		public void removeItem(int slot) {
			if (slot >= Inventory.assessInd) {
				menu.emptySpecialSlot(Inventory.slotToAssessSlot(slot));
			} else if (slot >= Inventory.hotbarInd) {
				hotbar.emptySlot(Inventory.slotToHotbarSlot(slot));
			} else {
				menu.emptySlot(slot);
			}
		}



		public void onClick(GameObject obj) {

			if (!menuActive)
				return;

			int slot = obj.GetComponent<InventoryCell>().slotNum;
			//checks if hotbar
			//if(obj)

			//is item already in cursor?
			if (InventoryFollowCursor.instance != null) {
				//is the slot selecting already filled
				if (PlayerData.instance.inventory.isItemPresent(slot)) {

					ItemStack cursI = InventoryFollowCursor.removeFromCursor();

					//are they of the same type
					if (PlayerData.instance.inventory.isSameType(slot, cursI.item)) {

						PlayerData.instance.inventory.mergeSlots(slot, cursI);

						//if (slot >= Inventory.assessInd) {
						//	menu.mergeSpecialItem(cursI, Inventory.slotToAssessSlot(slot));
						//} else if (slot >= Inventory.hotbarInd) {
						//	hotbar.mergeItem(cursI, Inventory.slotToHotbarSlot(slot));
						//} else {
						//	menu.mergeItem(cursI, slot);
						//}
						merge(slot, cursI);

					} else {
						//switch the items around
						//remove items
						ItemStack slotI = PlayerData.instance.inventory.removeItemInSlot(slot);

						//ui
						//is it hotbar?
						//if (slot >= Inventory.assessInd) {
						//	menu.emptySpecialSlot(Inventory.slotToAssessSlot(slot));
						//} else if (slot >= Inventory.hotbarInd) {
						//	//remove from hotbar
						//	hotbar.emptySlot(Inventory.slotToHotbarSlot(slot));
						//} else {
						//	//remove from main
						//	menu.emptySlot(slot);
						//}
						removeItem(slot);

						//put items
						PlayerData.instance.inventory.insertItem(slot, cursI);
						InventoryFollowCursor.setOnCursor(slotI, canvas, cursorObj);

						//ui
						//is it hotbar?
						//if (slot >= Inventory.assessInd) {
						//	menu.placeSpecialItem(cursI, Inventory.slotToAssessSlot(slot));
						//} else if (slot >= Inventory.hotbarInd) {
						//	//place to hotbar
						//	hotbar.placeItem(cursI, Inventory.slotToHotbarSlot(slot));
						//} else {
						//	//place to main
						//	menu.placeItem(cursI, slot);
						//}
						placeItem(slot, cursI);
					}
				} else {
					//place item in slot
					ItemStack cursI = InventoryFollowCursor.removeFromCursor();
					PlayerData.instance.inventory.insertItem(slot, cursI);

					//is it hotbar?
					//if (slot >= Inventory.assessInd) {
					//	menu.placeSpecialItem(cursI, Inventory.slotToAssessSlot(slot));
					//} else if (slot >= Inventory.hotbarInd) {
					//	//place to hotbar
					//	hotbar.placeItem(cursI, Inventory.slotToHotbarSlot(slot));
					//} else {
					//	//place to main
					//	menu.placeItem(cursI, slot);
					//}
					placeItem(slot, cursI);
				}
				
			} else if (PlayerData.instance.inventory.isItemPresent(slot)) {

				//remove from slot and put on cursor
				ItemStack item = PlayerData.instance.inventory.removeItemInSlot(slot);

				//is it hotbar?
				//if (slot >= Inventory.assessInd) {
				//	menu.emptySpecialSlot(Inventory.slotToAssessSlot(slot))
				//} else if (slot >= Inventory.hotbarInd) {
				//	//remove from hotbar
				//	hotbar.emptySlot(Inventory.slotToHotbarSlot(slot));
				//} else {
				//	//remove from main
				//	menu.emptySlot(slot);
				//}
				removeItem(slot);

				InventoryFollowCursor.setOnCursor(item, canvas, cursorObj);

			}
			//Debug.Log(PlayerData.instance.inventory.HotBarContents);
		}

		public void onSplitClick(GameObject obj) {
			if (!menuActive)
				return;

			int slot = obj.GetComponent<InventoryCell>().slotNum;
			Inventory inv = PlayerData.instance.inventory;

			//checks if item is in cursor
			if (!(InventoryFollowCursor.instance is null)) {

				InventoryFollowCursor cur = InventoryFollowCursor.instance;

				//item is in cursor

				//is there a item in the slot
				if (inv.isItemPresent(slot)) {
					//there is an item in the slot

					//are they the same type
					if (inv.isSameType(slot, cur.item.item)) {
						//they are the same type
						//move one item to slot
						InvItem i = cur.item.item;

						ItemStack iis = new ItemStack(cur.item.item, 1);

						cur.takeOne();
						inv.mergeSlots(slot, iis);

						merge(slot, iis);

					}
					//else ignore
					
				} else {
					//there is no item in the slot
					InvItem i = cur.item.item;
					cur.takeOne();

					ItemStack iis = new ItemStack(cur.item.item, 1);
					//place one item there
					//inv.mergeSlots(slot, iis);
					//merge(slot, iis);
					inv.insertItem(slot, iis);
					placeItem(slot, iis);
				}
			} else {
				//item is not in cursor
				//is there an item in the slot
				if (inv.isItemPresent(slot)) {
					//there is an item in the slot
					//grab half
					ItemStack i = inv.getItemInSlot(slot);
					ItemStack i2 = i.splitHalf();

					if (i2 != null) {
						InventoryFollowCursor.setOnCursor(i2, canvas, cursorObj);
						placeItem(slot, i);
					} else {
						InventoryFollowCursor.setOnCursor(i, canvas, cursorObj);
						inv.removeItemInSlot(slot);
						removeItem(slot);
					}

				}
				//else ignore

			}

		}

	}
}