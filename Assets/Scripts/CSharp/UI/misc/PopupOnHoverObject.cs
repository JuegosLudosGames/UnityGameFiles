using JLG.gift.cSharp.formating;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JLG.gift.cSharp.ui {
	public class PopupOnHoverObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

		public PopupType type;
		public GameObject prefab;
		public Canvas parent;
		public bool isEnabled = false;

		[Header("Text")]
		[TextArea]
		public string text;

		[Header("Image")]
		public Sprite image;
		public Color color;

		GraphicRaycaster m_Raycaster;
		PointerEventData m_PointerEventData;
		EventSystem m_EventSystem;
		//bool hoveredLast = false;

		private Popup obj;

		private void Start() {
			//Fetch the Raycaster from the GameObject (the Canvas)
			m_Raycaster = GetComponent<GraphicRaycaster>();
			//Fetch the Event System from the Scene
			m_EventSystem = GetComponent<EventSystem>();
		}

		//void Update() {
		//	//Set up the new Pointer Event
		//	m_PointerEventData = new PointerEventData(m_EventSystem);
		//	//Set the Pointer Event Position to that of the mouse position
		//	m_PointerEventData.position = Input.mousePosition;

		//	//Create a list of Raycast Results
		//	List<RaycastResult> results = new List<RaycastResult>();

		//	//Raycast using the Graphics Raycaster and mouse click position
		//	m_Raycaster.Raycast(m_PointerEventData, results);

		//	//For every result returned, output the name of the GameObject on the Canvas hit by the Ray
		//	foreach (RaycastResult result in results) {
		//		if (result.gameObject == gameObject) {
		//			if (!hoveredLast) {
		//				hoveredLast = true;
		//				OnPointerEnter();
		//				return;
		//			}
		//		}
		//	}
		//	if (hoveredLast) {
		//		hoveredLast = false;
		//		OnPointerExit();
		//		return;
		//	}
		//}

		public void OnPointerEnter(PointerEventData d) {
		//private void OnMouseOver() {
		//public void OnMouseEnter() { 
			if (isEnabled && obj is null) {
				Vector2 movePos;

				RectTransformUtility.ScreenPointToLocalPointInRectangle(
					parent.transform as RectTransform,
					Input.mousePosition, parent.worldCamera,
					out movePos);

				GameObject gen = GameObject.Instantiate(prefab, movePos, new Quaternion(0,0,0,0),parent.transform);
				obj = gen.GetComponent<Popup>();
				obj.parentCanvas = parent;
				if (type == PopupType.TEXT) {
					setupText();
				} else if (type == PopupType.IMAGE) {
					setupImage();
				}
			}
		}

		public void OnPointerExit(PointerEventData d) {
			//public void OnMouseExit() { 
			if (!(obj is null)) {
				obj.breakObj();
				obj = null;
			}

			//if (!(obj is null)) {
			//	//Set up the new Pointer Event
			//	m_PointerEventData = new PointerEventData(m_EventSystem);
			//	//Set the Pointer Event Position to that of the mouse position
			//	m_PointerEventData.position = Input.mousePosition;

			//	//Create a list of Raycast Results
			//	List<RaycastResult> results = new List<RaycastResult>();

			//	//Raycast using the Graphics Raycaster and mouse click position
			//	m_Raycaster.Raycast(m_PointerEventData, results);

			//	if (results.ToArray().Length == 0) {
			//		obj.breakObj();
			//		obj = null;
			//		return;
			//	}

			//	//For every result returned, output the name of the GameObject on the Canvas hit by the Ray
			//	foreach (RaycastResult result in results) {
			//		if (result.gameObject != gameObject) {
			//			//if (!hoveredLast) {
			//			//	hoveredLast = true;
			//			//	OnPointerEnter();
			//			//	return;
			//			//}
			//			obj.breakObj();
			//			obj = null;

			//		}
			//	}
			//}

			//Debug.Log("Checking");

			//if (!d.hovered.Contains(gameObject) && !(obj is null)) {
			//	//Debug.Log("Working");
			//	obj.breakObj();
			//	obj = null;
			//}

		}

		public enum PopupType {
			IMAGE, TEXT
		}

		private void setupText() {
			Text t = obj.GetComponentInChildren<Text>();
			t.text = TextFormatter.translateCodes(text);
		}

		private void setupImage() {
			Image i = obj.GetComponentInChildren<Image>();
			i.sprite = image;
			i.color = color;
		}

	}
}