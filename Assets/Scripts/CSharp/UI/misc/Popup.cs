using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.ui {
	public class Popup : MonoBehaviour {

		public Canvas parentCanvas;

		// Update is called once per frame
		void Update() {
			Vector2 movePos;

			RectTransformUtility.ScreenPointToLocalPointInRectangle(
				parentCanvas.transform as RectTransform,
				Input.mousePosition, parentCanvas.worldCamera,
				out movePos);

			transform.position = parentCanvas.transform.TransformPoint(movePos);
		}

		protected virtual void onUpdate() { }

		public void breakObj() {
			GameObject.Destroy(gameObject);
		}

	}
}