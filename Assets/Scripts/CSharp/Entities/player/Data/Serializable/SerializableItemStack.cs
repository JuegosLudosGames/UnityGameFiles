using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.inventory {
	[System.Serializable]
	public class SerializableItemStack {
		public int id;
		public int amount;

		public SerializableItemStack(int id, int amount) =>
			(this.id, this.amount) = (id, amount);

		public static implicit operator ItemStack(SerializableItemStack rValue) {
			InvItem i = InvItem.getAssetById(rValue.id);
			if (!(i is null))
				return new ItemStack(i, rValue.amount);
			else
				return null;
		}

		public static implicit operator SerializableItemStack(ItemStack rValue) {
			return new SerializableItemStack(rValue.item.Id, rValue.amount);
		}

	}
}