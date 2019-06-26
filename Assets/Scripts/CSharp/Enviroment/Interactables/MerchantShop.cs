using JLG.gift.cSharp.inventory.merchant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.enviroment.interactble {
	public class MerchantShop : Interactable {

		public MerchData MerchantShopData;

		protected override void onStart() {
			base.onStart();
			rangeEnable = true;
		}

		protected override void onInteract() {
			MerchantSystem ms = GameObject.FindGameObjectWithTag("MerchantSystem").GetComponent<MerchantSystem>();
			ms.activate();
			ms.currentMerchData = MerchantShopData;
		}
	}
}