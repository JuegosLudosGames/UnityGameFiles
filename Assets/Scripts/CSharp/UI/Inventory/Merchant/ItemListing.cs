using JLG.gift.cSharp.formating;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JLG.gift.cSharp.inventory.merchant {
	public class ItemListing : MonoBehaviour {
		public InvItem id;
		public Image Icon;
		public TextComponentFormatter Name;
		public Text Price;
		public Text Stock;
	}
}