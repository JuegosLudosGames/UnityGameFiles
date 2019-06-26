using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.inventory {
	public class Inventory {
		public static readonly int MaxItems = 50;
		public List<ItemStack> Contents {
			get; private set;
		}

		public static readonly int maxHotbarItems = 5;
		public static readonly int hotbarInd = 1 << 7;
		public List<ItemStack> HotBarContents {
			get; private set;
		}

		public static readonly int maxAssessoryItems = 4;
		public static readonly int assessInd = 1 << 8;
		public List<ItemStack> AssesContents {
			get; private set;
		}

		public int money;

		public void updateInv() {
			foreach (ItemStack i in Contents) {
				if(!(i is null))
					i.update();
			}
			foreach (ItemStack i in HotBarContents) {
				if (!(i is null))
					i.update();
			}
			foreach (ItemStack i in AssesContents) {
				if (!(i is null))
					i.update();
			}
		}

		public Inventory(List<ItemStack> contents, List<ItemStack> hotbar, List<ItemStack> assess, int m) {
			Contents = new List<ItemStack>();
			HotBarContents = new List<ItemStack>();
			AssesContents = new List<ItemStack>();

			for (int x = 0; x < MaxItems; x++) {
				if (x < contents.ToArray().Length)
					Contents.Insert(x, contents[x]);
				else
					Contents.Insert(x, null);
			}
			for (int x = 0; x < maxHotbarItems; x++) {
				if (x < hotbar.ToArray().Length)
					HotBarContents.Insert(x, hotbar[x]);
				else
					HotBarContents.Insert(x, null);
			}
			for (int x = 0; x < maxAssessoryItems; x++) {
				if (x < assess.ToArray().Length)
					AssesContents.Insert(x, assess[x]);
				else
					AssesContents.Insert(x, null);
			}
			money = m;
		}

		public ItemStack getItemInSlot(int slot) {
			return getlist(slot)[getProperSlot(slot)];
		}

		public ItemStack removeItemInSlot(int slot) {
			List<ItemStack> found = getlist(slot);
			int nSlot = getProperSlot(slot);
			if (found.Count > nSlot) {
				ItemStack item = found[nSlot];
				found[nSlot] = null;
				return item;
			} else {
				return null;
			}
		}

		public InvItem subtractItemInSlot(int slot, int count) {
			List<ItemStack> found = getlist(slot);
			int nSlot = getProperSlot(slot);
			if (found.Count > nSlot) {
				InvItem r = found[nSlot].item;
				if (found[nSlot].amount <= count) {
					found[nSlot] = null;
				} else {
					found[nSlot].amount -= count;
				}
				return r;
			} else {
				return null;
			}
		}

		public InvItem subtractItemInSlot(int slot) {
			return subtractItemInSlot(slot, 1);
		}

		public void insertItem(int slot, ItemStack item) {
			if (item.amount <= 0)
				return;

			List<ItemStack> found = getlist(slot);
			int nSlot = getProperSlot(slot);
			if (found.Count > nSlot) {
				found[nSlot] = item;
			} else {
				found.Insert(nSlot, item);
			}
		}

		public void mergeSlots(int slot, ItemStack item) {
			if (item.amount <= 0)
				return;

			List<ItemStack> found = getlist(slot);
			int nSlot = getProperSlot(slot);
			if (found.Count > nSlot && !(found[nSlot] is null)) {
				found[nSlot] += item;
			} else {
				//found.Insert(nSlot, item);
				insertItem(slot, item);
			}

		}

		public bool isSameType(int slot, InvItem item) {

			List<ItemStack> found = getlist(slot);
			int nSlot = getProperSlot(slot);
			if (found.Count > nSlot) {
				return found[nSlot].item == item;
			} else {
				return false;
			}

		}

		public bool placeInFirstAvaliable(ItemStack item) {

			int slot = isSpaceAvaliable();

			if (slot == -1)
				return false;

			insertItem(slot, item);

			return true;
		}

		public int isSpaceAvaliable() {
			//check hotbar
			for (int x = 0; x < maxHotbarItems; x++) {
				if (!isItemPresent(HotbarSlotToSlot(x))) 
					return HotbarSlotToSlot(x);
			}
			//check main inv
			for (int x = 0; x < MaxItems; x++) {
				if (!isItemPresent(x))
					return x;
			}
			return -1;
		}

		public int isSpaceAvaliable(InvItem item) {
			//check hotbar
			for (int x = 0; x < maxHotbarItems; x++) {
				if (!isItemPresent(HotbarSlotToSlot(x)) || getItemInSlot(HotbarSlotToSlot(x)).item == item)
					return HotbarSlotToSlot(x);
			}
			//check main inv
			for (int x = 0; x < MaxItems; x++) {
				if (!isItemPresent(x) || getItemInSlot(x).item == item)
					return x;
			}
			return -1;
		}

		public bool isItemPresent(int slot) {

			List<ItemStack> found = getlist(slot);
			int nSlot = getProperSlot(slot);
			if (found.Count > nSlot) {
				return !(found[nSlot] is null || found[nSlot].amount == 0);
			} else {
				return false;
			}

		}

		public static int slotToHotbarSlot(int slot) {
			return slot - hotbarInd;
		}

		public static int HotbarSlotToSlot(int slot) {
			return slot + hotbarInd;
		}

		public static int slotToAssessSlot(int slot) {
			return slot - assessInd;
		}

		public static int AssessSlotToSlot(int slot) {
			return slot + assessInd;
		}

		public static int getProperSlot(int slot) {
			if (slot >= assessInd) {
				return slotToAssessSlot(slot);
			} else if (slot >= hotbarInd) {
				return slotToHotbarSlot(slot);
			} else {
				return slot;
			}
		}

		public List<ItemStack> getlist(int slot) {
			if (slot >= assessInd) {
				return AssesContents;
			} else if (slot >= hotbarInd) {
				return HotBarContents;
			} else {
				return Contents;
			}
		}

		public List<AssessoryItem> GetAssessoryItems() {
			List<AssessoryItem> items = new List<AssessoryItem>();
			for (int x = 0; x < maxAssessoryItems; x++) {
				if (isItemPresent(AssessSlotToSlot(x))) {
					InvItem i = getItemInSlot(AssessSlotToSlot(x)).item;
					if (i is AssessoryItem) {
						items.Add(i as AssessoryItem);
					}
				}
			}
			return items;
		}

		public Dictionary<InvItem, int> mapInventoryContents() {
			Dictionary<InvItem, int> dic = new Dictionary<InvItem, int>();
			foreach (ItemStack i in AssesContents) {
				if (i is null)
					continue;
				if (dic.ContainsKey(i.item)) {
					dic[i.item] += i.amount;
				} else {
					dic.Add(i.item, i.amount);
				}
			}
			foreach (ItemStack i in HotBarContents) {
				if (i is null)
					continue;
				if (dic.ContainsKey(i.item)) {
					dic[i.item] += i.amount;
				} else {
					dic.Add(i.item, i.amount);
				}
			}
			foreach (ItemStack i in Contents) {
				if (i is null)
					continue;
				if (dic.ContainsKey(i.item)) {
					dic[i.item] += i.amount;
				} else {
					dic.Add(i.item, i.amount);
				}
			}
			return dic;
		}

		public int findFirstSlotOfItem(InvItem ii) {
			for (int x = 0; x < HotBarContents.Count; x++) {
				if (HotBarContents[x] is null) 
					continue;

				if (HotBarContents[x].item == ii)
					return HotbarSlotToSlot(x);
			}
			for (int x = 0; x < Contents.Count; x++) {
				if (Contents[x] is null)
					continue;

				if (Contents[x].item == ii)
					return x;
			}
			for (int x = 0; x < AssesContents.Count; x++) {
				if (AssesContents[x] is null)
					continue;

				if (AssesContents[x].item == ii)
					return x;
			}
			return -1;
		}

	}
}