using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace JLG.gift.cSharp.ui.overlay {
	[ExecuteAlways]
	public class CircularSlider : MonoBehaviour {

		public Image fillArea;
		public RectTransform handleRotatePoint;
		[Range(0.0f, 1.0f)]
		public float fillAmount = 1;

		// Update is called once per frame
		void Update() {
			fillArea.fillAmount = fillAmount;
			handleRotatePoint.eulerAngles = new Vector3(0, 0, -(fillAmount * 360));
		}
	}
}