using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JLG.gift.cSharp.ui.menuField {
	public class KeyField : MonoBehaviour, MenuField<KeyCode> {

		public Text textObj;
		public GameObject highlight;

		KeyCode currentValue;
		bool isSearching = false;

		private void Start() {
		}

		private void Update() {
			if (isSearching && Input.anyKeyDown) {
				foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode))) {
					if (Input.GetKeyDown(kcode)) {
						setStartValue(kcode);
						isSearching = false;
					}	
				}

			}
		}

		public KeyCode getValue() {
			return currentValue;
		}

		public void setStartValue(KeyCode value) {
			currentValue = value;
			textObj.text = value.ToString();
		}

		public void onClick() {
			if (!isSearching) {
				isSearching = true;
				highlight.SetActive(true);
				textObj.text = "press";
			}
		}

	}
}