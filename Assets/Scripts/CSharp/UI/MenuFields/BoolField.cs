using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JLG.gift.cSharp.ui.menuField {
	public class BoolField : MonoBehaviour, MenuField<bool> {

		public Toggle toggleObj;

		bool currentValue;

		public bool getValue() {
			return currentValue;
		}

		public void setStartValue(bool value) {
			currentValue = value;
			toggleObj.isOn = value;
		}

		public void onValueChanged() {
			currentValue = toggleObj.isOn;
		}
	}
}