using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.inventory {
	public class InvItemProbableDropper : ItemDropper {

		public float dropRange;

		public InvItem[] itemsToDrop;
		public byte[] amountToDrop;
		public byte[] chance;

		public override void drop() {
			for (int x = 0; x < itemsToDrop.Length; x++) {
				if (Random.Range(0, 100) <= chance[x]) {
					float place = Random.Range(-dropRange, dropRange);
					Pickupable.summonItem(new Vector3(place + transform.position.x, transform.position.y, transform.position.z), itemsToDrop[x], amountToDrop[x]);
				}
			}
		}
	}
}