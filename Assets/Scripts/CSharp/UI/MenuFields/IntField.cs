using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JLG.gift.cSharp.ui.menuField {
	public class IntField : MonoBehaviour, MenuField<int> {

		public InputField typeField;

		int currentValue;

		public int getValue() {
			return currentValue;
		}

		public void setStartValue(int value) {
			currentValue = value;
			typeField.text = value.ToString();
		}

		public void updateValue() {
			string value = typeField.text;
			int a = currentValue;
			if (!int.TryParse(value, out a))
				a = currentValue;
			setStartValue(a);
		}

		public void onClickUp() {
			setStartValue(currentValue + 1);
		}

		public void onClickDown() {
			setStartValue(currentValue - 1);
		}
	}
}