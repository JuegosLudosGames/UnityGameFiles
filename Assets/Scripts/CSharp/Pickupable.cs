using JLG.gift.cSharp.background.scene.background;
using JLG.gift.cSharp.inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.inventory {
	[RequireComponent(typeof(SpriteRenderer))]
	public class Pickupable : MonoBehaviour {

		public static void summonItem(Vector3 position, InvItem item, int amount) {
			GameObject parent = GameObject.FindGameObjectWithTag("QuickObjectHoler");
			GameObject got = GameObject.Instantiate(GlobalItems.instance.groundItemPrefab, position, new Quaternion(0, 0, 0, 0), parent.transform);
			//Pickupable r = got.GetComponent<Pickupable>();
			got.GetComponent<Pickupable>().item = item;
			got.GetComponent<Pickupable>().amount = amount;
			got.transform.localScale = new Vector3(item.scale, item.scale, item.scale);
		}

		public static void summonItem(Vector3 position, ItemStack item) {
			//GameObject parent = GameObject.FindGameObjectWithTag("QuickObjectHoler");
			//GameObject got = GameObject.Instantiate(GlobalItems.instance.groundItemPrefab, position, new Quaternion(0, 0, 0, 0), parent.transform);
			////Pickupable r = got.GetComponent<Pickupable>();
			//got.GetComponent<Pickupable>().item = item.item;
			//got.GetComponent<Pickupable>().amount = item.amount;
			summonItem(position, item.item, item.amount);
		}

		public InvItem item;
		public int amount;

		void Start() {
			GetComponent<SpriteRenderer>().sprite = item.Icon;
			transform.localScale = new Vector3(item.scale, item.scale, item.scale);
			//GetComponent<Animation>().lo
		}

		public ItemStack pickup() {
			GameObject.Destroy(gameObject);
			return new ItemStack(item, amount == 0 ? 1 : Mathf.Abs(amount));
		}

	}
}