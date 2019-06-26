using JLG.gift.cSharp.background.scene.background;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.buildData {
	public class BuildSelection : MonoBehaviour {

		public GameObject contents;
		private GameObject returnTo;

		public void StartSelection(BuildData[] bd, GameObject returnTo) {
			foreach (BuildData b in bd) {
				GameObject obj = (GameObject) GameObject.Instantiate(GlobalItems.instance.BuildSelListingPrefab, contents.transform);
				BuildSelectionListing listing = obj.GetComponent<BuildSelectionListing>();
				listing.icon.sprite = b.Icon;
				listing.icon.color = b.IconColor;
				listing.description.updateText(b.Description);
				
			}
			this.returnTo = returnTo;
		}

	}
}