using JLG.gift.cSharp.background.scene.background;
using JLG.gift.cSharp.formating;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace JLG.gift.cSharp.inventory.merchant {
	public class MerchantSystemScreen : MonoBehaviour {

		public bool isBuying;
		public Image ItemPreview;
		public TextComponentFormatter ItemDescription;
		public GameObject listingContainer;
		public Text money;
		public Dictionary<InvItem, int> shopAndStock;
		public MerchantSystem ms;
		[HideInInspector]
		public int moneyVal;

		//only updates values of listings
		public void softUpdate() {
			List<GameObject> toDestroy = new List<GameObject>();
			foreach (ItemListing listing in listingContainer.GetComponentsInChildren<ItemListing>()) {
				if (!shopAndStock.ContainsKey(listing.id)) {
					toDestroy.Add(listing.gameObject);
					continue;
				}
				listing.Stock.text = shopAndStock[listing.id].ToString();
			}
			for (int x = 0; x < toDestroy.Count; x++) {
				GameObject.Destroy(toDestroy[x]);
			}
			money.text = moneyVal.ToString();
		}

		//updates fully
		public void hardUpdate() {
			//reset imagePreview
			ItemPreview.color = new Color(1, 1, 1, 0);

			//clears item desp
			ItemDescription.updateText("");

			//clears listings
			foreach (Transform child in listingContainer.transform) {
				GameObject.Destroy(child.gameObject);
			}

			//adds merchant listings
			if (isBuying)
				fillListingBuy();
			else
				fillListingSell();

			money.text = moneyVal.ToString();

		}

		private void fillListingBuy() {
			//goes through every item with stock of player
			foreach (InvItem key in shopAndStock.Keys) {
				//clones listing
				GameObject o = GameObject.Instantiate(GlobalItems.instance.ItemListingPrefab, listingContainer.transform);
				ItemListing i = o.GetComponent<ItemListing>();
				
				//sets values
				i.id = key;
				i.Icon.sprite = key.Icon;
				//i.Name.text = key.DisplayName;
				i.Name.updateText(key.DisplayName);
				i.Price.text = key.BuyCost.ToString();
				i.Stock.text = shopAndStock[key].ToString();

				o.GetComponent<Button>().onClick.AddListener(delegate { clickListing(o); });
				o.GetComponent<EventTrigger>().triggers[0].callback.AddListener(delegate { hoverListing(o); });
				
			}
		}

		private void fillListingSell() {
			//goes through every item with stock of player
			foreach (InvItem key in shopAndStock.Keys) {
				//clones listing
				GameObject o = GameObject.Instantiate(GlobalItems.instance.ItemListingPrefab, listingContainer.transform);
				ItemListing i = o.GetComponent<ItemListing>();

				//sets values
				i.id = key;
				i.Icon.sprite = key.Icon;
				//i.Name.text = key.DisplayName;
				i.Name.updateText(key.DisplayName);
				i.Price.text = key.SellCost.ToString();
				i.Stock.text = shopAndStock[key].ToString();

				o.GetComponent<Button>().onClick.AddListener(delegate { clickListing(o); });
				o.GetComponent<EventTrigger>().triggers[0].callback.AddListener(delegate { hoverListing(o); });
			}
		}

		public void hoverListing(GameObject o) {
			ItemListing i = o.GetComponent<ItemListing>();

			ItemPreview.sprite = i.id.Icon;
			ItemDescription.updateText(i.id.SellDescription);

			ItemPreview.color = Color.white;
		}

		public void clickListing(GameObject o) {
			ItemListing i = o.GetComponent<ItemListing>();
			if (isBuying)
				ms.buyItem(i.id);
			else
				ms.sellItem(i.id);
		}

	}
}