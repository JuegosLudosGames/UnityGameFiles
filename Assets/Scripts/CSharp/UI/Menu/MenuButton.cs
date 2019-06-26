using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace JLG.gift.cSharp.ui.menu {
	[RequireComponent(typeof(Button))]
	public class MenuButton : MonoBehaviour {

		public ScreenMenu menu;
		public GameObject current;
		public GameObject sendTo;

		// Start is called before the first frame update
		void Start() {
			UnityAction action = onAction;
			this.GetComponent<Button>().onClick.AddListener(action);
		}

		public void onAction() {
			menu.sendTo(current, sendTo);
		}

	}
}