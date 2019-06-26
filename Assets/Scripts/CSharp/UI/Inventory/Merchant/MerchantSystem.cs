using JLG.gift.cSharp.background.scene;
using JLG.gift.cSharp.entity.player.data;
using JLG.gift.cSharp.ui.inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.inventory.merchant {
	public class MerchantSystem : MonoBehaviour {

		public GameObject main;
		public MerchantSystemScreen buy;
		public MerchantSystemScreen sell;
		[HideInInspector]
		public MerchData currentMerchData;

		Inventory inv;

		private void Start() {
			main.SetActive(false);
			buy.gameObject.SetActive(false);
			sell.gameObject.SetActive(false);
		}

		public void buyItem(InvItem i) {
			int foundSlot = inv.isSpaceAvaliable(i);
			if (foundSlot == -1) {
				background.scene.background.MessageBox.instance.messageCritical("Oops, you dont have enough space in your inventory to buy that");
				return;
			}

			if (inv.money < i.BuyCost) {
				background.scene.background.MessageBox.instance.messageWarning("You dont have enough money to buy that");
			}

			//subtract money
			inv.money -= i.BuyCost;

			buy.moneyVal = inv.money;

			//add to inv
			inv.mergeSlots(foundSlot, new ItemStack(i, 1));

			Dictionary<InvItem, int> p = inv.mapInventoryContents();
			Dictionary<InvItem, int> t = new Dictionary<InvItem, int>();

			foreach (InvItem ii in currentMerchData.ItemsToSell) {
				if (p.ContainsKey(ii)) {
					t.Add(ii, p[ii]);
				} else {
					t.Add(ii, 0);
				}
			}

			buy.shopAndStock = t;
			buy.moneyVal = inv.money;

			//update the screen
			buy.softUpdate();
		}

		public void sellItem(InvItem i) {
			//verify item is in inv
			int foundSlot = inv.findFirstSlotOfItem(i);
			if (foundSlot == -1) {
				background.scene.background.MessageBox.instance.messageCritical("Oops, Something went wrong, Error 5000");
				Debug.LogError("Item somehow stopped existing inside inventory");
				return;
			}

			//add money
			inv.money += i.SellCost;
			sell.moneyVal = inv.money;

			//remove from inv
			inv.subtractItemInSlot(foundSlot);

			//update
			sell.shopAndStock = inv.mapInventoryContents();
			sell.moneyVal = inv.money;
			sell.softUpdate();
		}

		public void goMain() {
			main.SetActive(true);
			buy.gameObject.SetActive(false);
			sell.gameObject.SetActive(false);
		}

		public void leave() {
			main.SetActive(false);
			buy.gameObject.SetActive(false);
			sell.gameObject.SetActive(false);
			UIReferences r = GameObject.FindGameObjectWithTag("UIInstance").GetComponent<UIReferences>();
			//GameObject.FindGameObjectWithTag("InventorySystem").SetActive(true);
			//GameObject.FindGameObjectWithTag("InventorySystem").GetComponent<InventorySystem>().updateInventory();
			//GameObject.FindGameObjectWithTag("PauseSystem").SetActive(true);
			//GameObject.FindGameObjectWithTag("Overlay").SetActive(true);
			r.InventorySystem.SetActive(true);
			r.InventorySystem.GetComponent<InventorySystem>().updateInventory();
			r.PauseSystem.SetActive(true);
			r.Overlay.SetActive(true);
			SceneController.currentScene.enableEntites = true;
		}

		public void activate() {
			goMain();
			inv = PlayerData.instance.inventory;
			//GameObject.FindGameObjectWithTag("Overlay").SetActive(false);
			//GameObject.FindGameObjectWithTag("PauseSystem").SetActive(false);
			//GameObject.FindGameObjectWithTag("InventorySystem").SetActive(false);
			UIReferences r = GameObject.FindGameObjectWithTag("UIInstance").GetComponent<UIReferences>();
			r.InventorySystem.SetActive(false);
			r.PauseSystem.SetActive(false);
			r.Overlay.SetActive(false);
			SceneController.currentScene.enableEntites = false;
		}

		public void openBuyScreen() {
			main.SetActive(false);
			buy.gameObject.SetActive(true);
			sell.gameObject.SetActive(false);

			//buy.shopAndStock = inv.mapInventoryContents();
			buy.moneyVal = inv.money;

			Dictionary<InvItem, int> p = inv.mapInventoryContents();
			Dictionary<InvItem, int> t = new Dictionary<InvItem, int>();

			foreach (InvItem i in currentMerchData.ItemsToSell) {
				if (p.ContainsKey(i)) {
					t.Add(i, p[i]);
				} else {
					t.Add(i, 0);
				}
			}

			buy.shopAndStock = t;

			buy.hardUpdate();
		}

		public void openSellScreen() {
			main.SetActive(false);
			buy.gameObject.SetActive(false);
			sell.gameObject.SetActive(true);

			sell.shopAndStock = inv.mapInventoryContents();
			//Dictionary<InvItem, int> p = inv.mapInventoryContents();
			//Dictionary<InvItem, int> t = new Dictionary<InvItem, int>();

			//foreach (InvItem i in currentMerchData.ItemsToSell) {
			//	if (p.ContainsKey(i)) {
			//		t.Add(i, p[i]);
			//	} else {
			//		t.Add(i, 0);
			//	}
			//}

			sell.moneyVal = inv.money;

			sell.hardUpdate();
		}

	}
}