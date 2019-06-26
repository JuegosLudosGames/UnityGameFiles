using JLG.gift.cSharp.inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.inventory {
	public class ItemStack {
		public InvItem item;
		public int amount;

		public float timeLeft;
		public float maxTime;
		public bool isCooling;

		public ItemStack(InvItem item, int amount) =>
			(this.item, this.amount) = (item, amount);

		public void cooldownSet(float startTime, float maxTime) {
			timeLeft = startTime;
			this.maxTime = maxTime;
			isCooling = true;
		}

		public void update() {
			if(isCooling) { 
				timeLeft -= Time.deltaTime;
				isCooling = timeLeft > 0;
			}
		}

		public ItemStack splitHalf() {
			if (amount > 1) {

				int give = amount / 2;
				amount -= give;

				return new ItemStack(item, give);
			} else {
				return null;
			}
		}

		public override string ToString() {
			return item.name;
		}

		public static ItemStack operator +(ItemStack ls, ItemStack rs) {
			if (ls.item != rs.item)
				throw new System.InvalidOperationException();
			return new ItemStack(ls.item, ls.amount + rs.amount);
		}

		public static ItemStack operator -(ItemStack ls, ItemStack rs) {
			if (ls.item != rs.item)
				throw new System.InvalidOperationException();
			return new ItemStack(ls.item, ls.amount - rs.amount);
		}

	}
}