using JLG.gift.cSharp.inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JLG.gift.cSharp.formating;
using JLG.gift.cSharp.inventory;
using JLG.gift.cSharp.entity.player.data;

namespace JLG.gift.cSharp.ui.inventory {
	public class InventoryMenu : MonoBehaviour {

		public GameObject mainSlotContainer;
		public GameObject specialSlotContainer;

		public int length {
			get; private set;
		}

		private InventoryCell[] mainSlots;
		private InventoryCell[] specialSlots;

		// Start is called before the first frame update
		void Start() {
			//mainSlots = new InventoryCell[mainSlotContainer.transform.childCount];
			//for (int x = 0; x < mainSlotContainer.transform.childCount; x++) {
			//	mainSlots[x] = mainSlotContainer.transform.GetChild(x).GetComponent<InventoryCell>();
			//	mainSlots[x].slotNum = x;
			//	mainSlots[x].lowAlpha();
			//	//emptySlot(mainSlots[x].slotNum);
			//	mainSlots[x].icon = null;
			//	mainSlots[x].amount = 0;
			//	mainSlots[x].clearPopup();
			//}

			mainSlots = new InventoryCell[5 * mainSlotContainer.transform.childCount];

			int counter = 0;
			for (int y = 0; y < mainSlotContainer.transform.childCount; y++) {
				Transform row = mainSlotContainer.transform.GetChild(y);
				for (int x = 0; x < row.childCount; x++) {
					mainSlots[counter] = row.GetChild(x).GetComponent<InventoryCell>();
					mainSlots[counter].slotNum = counter;
					mainSlots[counter].lowAlpha();
					//emptySlot(mainSlots[x].slotNum);
					mainSlots[counter].icon = null;
					mainSlots[counter].amount = 0;
					mainSlots[counter].clearPopup();
					counter++;
				}
			}

			length = counter;

			specialSlots = new InventoryCell[specialSlotContainer.transform.childCount];
			for (int x = 0; x < specialSlotContainer.transform.childCount; x++) {
				specialSlots[x] = specialSlotContainer.transform.GetChild(x).GetComponent<InventoryCell>();
				specialSlots[x].slotNum = Inventory.AssessSlotToSlot(x);
				specialSlots[x].lowAlpha();
				//emptySpecialSlot(specialSlots[x].slotNum);
				specialSlots[x].icon = null;
				specialSlots[x].amount = 0;
				specialSlots[x].clearPopup();

			}
			length = mainSlots.Length;
			resetScroll();
		}

		private void Update() {
			Inventory inv = PlayerData.instance.inventory;
			foreach (InventoryCell cell in mainSlots) {
				if (!(cell is null) && inv.isItemPresent(cell.slotNum)) {
					ItemStack it = inv.getItemInSlot(cell.slotNum);

					if (it.isCooling)
						cell.setfade(1 - (it.timeLeft / it.maxTime));
				}
			}
			foreach (InventoryCell cell in specialSlots) {
				if (!(cell is null) && inv.isItemPresent(cell.slotNum)) {
					ItemStack it = inv.getItemInSlot(cell.slotNum);

					if (it.isCooling)
						cell.setfade(1 - (it.timeLeft / it.maxTime));
				}
			}
		}

		public void resetScroll() {
			mainSlotContainer.GetComponent<RectTransform>().GetComponentInParent<ScrollRect>().verticalScrollbar.value = 1;
		}

		public InventoryCell[] getMainSlots() {
			return mainSlots;
		}

		public InventoryCell[] getSpecialSlots() {
			return specialSlots;
		}

		public void updateMainSlots(InventoryCell[] slots) {
			mainSlots = slots;
		}

		public void updateSpecialSlots(InventoryCell[] slots) {
			specialSlots = slots;
		}

		public void placeItem(ItemStack item, int slot) {
			if (!(item is null)) {
				if (item.amount <= 0)
					return;
			}

			//slots[slot].icon = (item == null)? null : item.Icon;
			if (item != null) {
				mainSlots[slot].icon = item.item.Icon;
				mainSlots[slot].amount = item.amount;
				mainSlots[slot].highAlpha();
				mainSlots[slot].setPopup(item.item.FullTag);
			} else {
				mainSlots[slot].icon = null;
				mainSlots[slot].amount = 0;
				mainSlots[slot].lowAlpha();
				mainSlots[slot].clearPopup();
			}

		}

		public void mergeItem(ItemStack item, int slot) {
			if (!(item is null)) {
				if (item.amount <= 0)
					return;
			}
			if (item != null) {
				if (mainSlots[slot].icon != null) {
					mainSlots[slot].amount += item.amount;
				} else {
					placeItem(item, slot);
				}
			}
		}

		public void emptySlot(int slot) {
			placeItem(null, slot);
		}

		public void placeSpecialItem(ItemStack item, int slot) {
			if (!(item is null)) {
				if (item.amount <= 0)
					return;
			}
			//slots[slot].icon = (item == null)? null : item.Icon;
			if (item != null) {
				specialSlots[slot].icon = item.item.Icon;
				specialSlots[slot].amount = item.amount;
				specialSlots[slot].highAlpha();
				specialSlots[slot].setPopup(item.item.FullTag);
			} else {
				specialSlots[slot].icon = null;
				specialSlots[slot].amount = 0;
				specialSlots[slot].lowAlpha();
				specialSlots[slot].clearPopup();
			}

		}

		public void mergeSpecialItem(ItemStack item, int slot) {
			if (!(item is null)) {
				if (item.amount <= 0)
					return;
			}
			if (item != null) {
				if (specialSlots[slot].icon != null) {
					specialSlots[slot].amount += item.amount;
				} else {
					placeSpecialItem(item, slot);
				}
			}
		}

		public void emptySpecialSlot(int slot) {
			placeSpecialItem(null, slot);
		}

		//public void placeItemSpecial(InvItem item, int slot) {
		//	specialSlots[slot].icon = (item == null) ? null : item.Icon;
		//}

		//public void emptySlotSpecial(int slot) {
		//	placeItem(null, slot);
		//}

	}
}