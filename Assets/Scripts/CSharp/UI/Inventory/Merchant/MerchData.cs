using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.inventory.merchant {
	[CreateAssetMenu(fileName = "new MerchData", menuName = "MerchData", order = 70)]
	public class MerchData : ScriptableObject {
		[SerializeField]
		private InvItem[] itemsToSell;

		public InvItem[] ItemsToSell { get { return itemsToSell; } }
	}
}