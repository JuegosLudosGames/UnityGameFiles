using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JLG.gift.cSharp.ui.menuField {
	public class IntRange : MonoBehaviour, MenuField<int> {

		public Slider slider;
		public int min = 0;
		public int max = 100;
		
		int currentValue;

		public int getValue() {
			return currentValue;
		}

		public void setStartValue(int value) {
			currentValue = value;
			slider.value = value / max;
		}
		
		public void onValueChange() {
			currentValue = (int)(100f * (slider.value));
		}
	}
}