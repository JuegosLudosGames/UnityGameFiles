using JLG.gift.cSharp.background.scene.background;
using JLG.gift.cSharp.inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JLG.gift.cSharp.enviroment.intertriggerables {
	public class ItemLockPopup : MonoBehaviour {

		public GameObject container;

		public void pop(InvItem a, InvItem b = null, InvItem c = null, int more = 0) {

			empty();

			createObject(a.Icon);
			if (!(b is null))
				createObject(b.Icon);
			if (!(c is null))
				createObject(c.Icon);
			if (more > 0)
				moreObject(more);

		}

		private void empty() {
			Transform[] c = container.GetComponentsInChildren<Transform>();
			for (int x = 0; x < c.Length; x++) {
				if(c[x] != container.transform)
					GameObject.Destroy(c[x].gameObject);
			}
		}

		private void createObject(Sprite s) {
			GameObject g = GameObject.Instantiate(GlobalItems.instance.ItemLockItemPrefab, container.transform);
			g.GetComponent<Image>().sprite = s;
		}

		private void moreObject(int m) {
			GameObject g = GameObject.Instantiate(GlobalItems.instance.ItemLockItemMorePrefab, container.transform);
			g.GetComponentInChildren<TextMesh>().text = m.ToString();
		}

	}
}