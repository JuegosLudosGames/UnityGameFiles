using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 using UnityEngine.EventSystems;
using UnityEngine.Events;
using JLG.gift.cSharp.ui.overlay;

namespace JLG.gift.cSharp.ui.inventory {
	public class InventoryCell : MonoBehaviour {

		public GradualFader i;
		public Text t;
		[SerializeField]
		private PopupOnHoverObject p;

		public int slotNum {
			get; internal set;
		}

		public Sprite icon {
			get {
				return i.sourceImage;
			}
			set {
				i.sourceImage = value;
			}
		}

		public int amount {
			get {
				return a;
			}
			set {
				t.text = value == 0? null : value.ToString();
				a = value;
			}
		}

		private int a;

		public void highAlpha() {
			i.sourceColor = new Color(1, 1, 1, 1);
		}

		public void lowAlpha() {
			i.sourceColor = new Color(1, 1, 1, 0);
		}

		public void setfade(float v) {
			i.fill = v;
		}

		public void clearPopup() {
			p.text = null;
			p.isEnabled = false;
		}

		public void setPopup(string text) {
			p.text = text;
			p.isEnabled = true;
		}

		// Start is called before the first frame update
		void Start() {
			//i = GetComponent<Image>();
			//i = GetComponentsInChildren<Image>()[1];
			//t = GetComponentInChildren<Text>();
			p.isEnabled = false;
		}

		

		

	}
}