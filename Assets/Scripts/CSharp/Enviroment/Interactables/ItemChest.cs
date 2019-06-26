using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JLG.gift.cSharp.inventory;
using JLG.gift.cSharp.background.scene;
using UnityEditor;

namespace JLG.gift.cSharp.enviroment.interactble {
	[RequireComponent(typeof(SpriteRenderer), typeof(ItemDropper), typeof(ISceneObjectData))]
	public class ItemChest : Interactable {

		public Sprite closed;
		public Sprite open;

		public Color closedC;
		public Color openC;

		public GameObject popup;

		private SpriteRenderer sr;
		private ItemDropper dr;
		private bool canDrop = false;

		ISceneObjectData isod;

		protected override void onStart() {
			sr = GetComponent<SpriteRenderer>();
			dr = GetComponent<ItemDropper>();
			popup.SetActive(false);
			rangeEnable = true;
			isod = GetComponent<ISceneObjectData>();
		}

		public void forceReEnable() {
			enable = true;
			rangeEnable = true;
			isod.stateData.state = 0;
		}

		protected override void onDisable() {
			sr.sprite = open;
			sr.color = openC;
		}

		protected override void onEnable() {
			sr.sprite = closed;
			sr.color = closedC;
		}

		protected override void onInteract() {
			if (canDrop) {
				dr.drop();
				enable = false;
				rangeEnable = false;
				isod.stateData.state = 1;
			}
		}

		protected override void whenLeaveRange() {
			base.whenLeaveRange();
			canDrop = false;
			popup.SetActive(false);
		}

		protected override void whenEnterRange() {
			base.whenEnterRange();
			canDrop = true;
			popup.SetActive(true);
		}

		protected override void onRangeDisable() {
			popup.SetActive(false);
		}

		public void onDataLoad() {
			isod = GetComponent<ISceneObjectData>();
			if (isod.stateData.state == 1) {
				enable = false;
				rangeEnable = false;

				isStartingEnabled = false;
			}
		}
	}
}