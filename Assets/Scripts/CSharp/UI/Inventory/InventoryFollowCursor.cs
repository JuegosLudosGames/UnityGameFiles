using JLG.gift.cSharp.inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JLG.gift.cSharp.ui.inventory {
	public class InventoryFollowCursor : MonoBehaviour {

		public static InventoryFollowCursor instance;

		public static ItemStack removeFromCursor() {
			ItemStack i = instance.item;
			InventoryFollowCursor c = instance;
			instance = null;
			GameObject.Destroy(c.gameObject);
			return i;
		}

		public static void setOnCursor(ItemStack item, Canvas canvas, GameObject prefab) {

			Vector2 movePos;

			RectTransformUtility.ScreenPointToLocalPointInRectangle(
				canvas.transform as RectTransform,
				Input.mousePosition, canvas.worldCamera,
				out movePos);

			InventoryFollowCursor obj = GameObject.Instantiate(prefab, movePos, new Quaternion(0,0,0,0),canvas.transform).GetComponent<InventoryFollowCursor>();
			instance = obj;
			instance.item = item;
			instance.parentCanvas = canvas;
			instance.start();
		}

		public Canvas parentCanvas;

		public ItemStack item {
			get; private set;
		}

		public void takeOne() {
			item.amount--;
			if (item.amount == 0)
				removeFromCursor();
		}

		private Image im;

		void start() {
			im = GetComponent<Image>();
			im.sprite = item.item.Icon;
		}

		// Update is called once per frame
		void Update() {
			Vector2 movePos;

			RectTransformUtility.ScreenPointToLocalPointInRectangle(
				parentCanvas.transform as RectTransform,
				Input.mousePosition, parentCanvas.worldCamera,
				out movePos);

			transform.position = parentCanvas.transform.TransformPoint(movePos);
		}
	}
}