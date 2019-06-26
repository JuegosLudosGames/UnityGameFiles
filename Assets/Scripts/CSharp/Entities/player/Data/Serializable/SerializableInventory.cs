using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JLG.gift.cSharp.inventory;
using JLG.gift.cSharp.background;

namespace JLG.gift.cSharp.inventory {
	[System.Serializable]
	public class SerializableInventory {

		//public Dictionary<int, SerializableItemStack> MainContents;
		//public Dictionary<int, SerializableItemStack> HotbarContents;
		//public Dictionary<int, SerializableItemStack> AssessoryContents;
		public SerializableJLGDictionary MainContents;
		public SerializableJLGDictionary HotbarContents;
		public SerializableJLGDictionary AssessoryContents;
		public int money;

		public SerializableInventory(List<ItemStack> contents, List<ItemStack> hotbar, List<ItemStack> assessory, int m) {

			Dictionary<int, SerializableItemStack> mainContents = new Dictionary<int, SerializableItemStack>();
			Dictionary<int, SerializableItemStack> hotbarContents = new Dictionary<int, SerializableItemStack>();
			Dictionary<int, SerializableItemStack> assessoryContents = new Dictionary<int, SerializableItemStack>();

			ItemStack[] items = contents.ToArray();
			for (int x = 0; x < items.Length; x++) {
				if (items[x] != null) {
					mainContents.Add(x, items[x]);
				}
			}

			items = hotbar.ToArray();
			for (int x = 0; x < items.Length; x++) {
				if (items[x] != null) {
					hotbarContents.Add(x, items[x]);
				}
			}

			items = assessory.ToArray();
			for (int x = 0; x < items.Length; x++) {
				if (items[x] != null) {
					assessoryContents.Add(x, items[x]);
				}
			}

			MainContents = mainContents;
			HotbarContents = hotbarContents;
			AssessoryContents = assessoryContents;

			money = m;
		}

		public static implicit operator SerializableInventory(Inventory rValue) {
			return new SerializableInventory(rValue.Contents, rValue.HotBarContents, rValue.AssesContents, rValue.money);
		}

		public static implicit operator Inventory(SerializableInventory rValue) {

			Dictionary<int, SerializableItemStack> mainContents = rValue.MainContents;
			Dictionary<int, SerializableItemStack> hotbarContents = rValue.HotbarContents;
			Dictionary<int, SerializableItemStack> assessoryContents = rValue.AssessoryContents;


			List<ItemStack> ret = new List<ItemStack>();
			foreach (int key in mainContents.Keys) {
				SerializableItemStack val;
				mainContents.TryGetValue(key, out val);
				ret.Insert(key, val);
			}
			List<ItemStack> ret2 = new List<ItemStack>();
			foreach (int key in hotbarContents.Keys) {
				SerializableItemStack val;
				hotbarContents.TryGetValue(key, out val);
				ret2.Insert(key, val);
			}
			List<ItemStack> ret3 = new List<ItemStack>();
			foreach (int key in assessoryContents.Keys) {
				SerializableItemStack val;
				assessoryContents.TryGetValue(key, out val);
				ret3.Insert(key, val);
			}
			return new Inventory(ret, ret2, ret3, rValue.money);
		}

	}
}