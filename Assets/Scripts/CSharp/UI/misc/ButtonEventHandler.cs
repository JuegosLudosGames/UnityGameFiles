using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace JLG.gift.cSharp.ui {
	public class ButtonEventHandler : MonoBehaviour, IPointerClickHandler {

		public UnityEvent leftClick;
		public UnityEvent middleClick;
		public UnityEvent rightClick;

		//public PointerEventData prevData {
		//	get; private set;
		//}

		public void OnPointerClick(PointerEventData eventData) {
			//Debug.Log("working" + eventData.button);
			//prevData = eventData;
			if (eventData.button == PointerEventData.InputButton.Left)
				leftClick.Invoke();
			else if (eventData.button == PointerEventData.InputButton.Middle)
				middleClick.Invoke();
			else if (eventData.button == PointerEventData.InputButton.Right)
				rightClick.Invoke();
		}
	}
}