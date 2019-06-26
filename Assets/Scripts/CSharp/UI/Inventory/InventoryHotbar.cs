using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JLG.gift.cSharp.inventory;
using JLG.gift.cSharp.formating;
using JLG.gift.cSharp.entity.player.data;

namespace JLG.gift.cSharp.ui.inventory {
	public class InventoryHotbar : MonoBehaviour {

		public GameObject slotContainer;

		public int length {
			get; private set;
		}

		private InventoryCell[] slots;

		private void Start() {
			slots = new InventoryCell[slotContainer.transform.childCount];
			for (int x = 0; x < slotContainer.transform.childCount; x++) {
				slots[x] = slotContainer.transform.GetChild(x).GetComponent<InventoryCell>();
				slots[x].slotNum = x + Inventory.hotbarInd;
				slots[x].lowAlpha();
				//emptySlot(slots[x].slotNum);
				slots[x].icon = null;
				slots[x].amount = 0;
				slots[x].clearPopup();
			}
			length = slots.Length;
		}

		private void Update() {
			Inventory inv = PlayerData.instance.inventory;
			foreach (InventoryCell cell in slots) {
				if (!(cell is null) && inv.isItemPresent(cell.slotNum)) {
					ItemStack it = inv.getItemInSlot(cell.slotNum);

					if(it.isCooling)
						cell.setfade(1 - (it.timeLeft / it.maxTime));
				}
			}
		}

			public void enableAllPops(Inventory inv) {
			foreach (InventoryCell cell in slots) {
				if (inv.isItemPresent(cell.slotNum)) {
					InvItem item = inv.getItemInSlot(cell.slotNum).item;
					//cell.setPopup(TextFormatter.combineDisplayTag(item.DisplayName, item.Tag));
					cell.setPopup(item.FullTag);
				}
			}
		}

		public void disableAllPops() {
			foreach (InventoryCell cell in slots) {
				cell.clearPopup();
			}
		}

		public void placeItem(ItemStack item, int slot) {
			if (!(item is null)) {
				if (item.amount <= 0)
					return;
			}
			//slots[slot].icon = (item == null)? null : item.Icon;
			if (item != null) {
				slots[slot].icon = item.item.Icon;
				slots[slot].amount = item.amount;
				slots[slot].highAlpha();
				slots[slot].setPopup(item.item.FullTag);
			} else {
				slots[slot].icon = null;
				slots[slot].amount = 0;
				slots[slot].lowAlpha();
				slots[slot].clearPopup();
			}
			//set cel to high alpha
			
		}

		public void mergeItem(ItemStack item, int slot) {
			if (!(item is null)) {
				if (item.amount <= 0)
					return;
			}
			if (item != null) {
				if (slots[slot].icon != null) {
					slots[slot].amount += item.amount;
				} else {
					placeItem(item, slot);
				}
			}
		}

		public void emptySlot(int slot) {
			placeItem(null, slot);
		}

		public RectTransform getTransform(int slot) {
			return slots[slot].GetComponent<RectTransform>();
		}

	}
}