using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JLG.gift.cSharp.enviroment.interactble;
using JLG.gift.cSharp.enviroment.triggerable;
using JLG.gift.cSharp.ui.inventory;
using JLG.gift.cSharp.entity.player.data;
using JLG.gift.cSharp.inventory;

namespace JLG.gift.cSharp.enviroment.intertriggerables {
	[RequireComponent(typeof(Trigger))]
	public class ItemLock : Interactable {

		public List<KeyItem> keyIds;
		public ItemLockPopup popup;
		private Trigger trig;
		private bool active = true;

		protected override void onInteract() {
			if (active) {
				InventorySystem invSys = GameObject.FindGameObjectWithTag("InventorySystem").GetComponent<InventorySystem>();
				Inventory inv = PlayerData.instance.inventory;
				ItemStack s = inv.getItemInSlot(Inventory.HotbarSlotToSlot(invSys.selectedSlot));

				if (s.item is KeyItem) {
					KeyItem k = s.item as KeyItem;
					if (keyIds.Contains(k)) {
						//clear the slot
						inv.removeItemInSlot(Inventory.HotbarSlotToSlot(invSys.selectedSlot));
						invSys.removeItem(Inventory.HotbarSlotToSlot(invSys.selectedSlot));
						keyIds.Remove(k);
						
						if (keyIds.Count == 0) {
							active = false;
							rangeEnable = false;
							trig.triggerAll();
						}
					}
				}
			}
		}

		protected override void onStart() {
			base.onStart();
			trig = GetComponent<Trigger>();
			popup.gameObject.SetActive(false);
			rangeEnable = true;
		}

		protected override void onUpdate() {
			base.onUpdate();
		}

		protected override void whenLeaveRange() {
			base.whenLeaveRange();
			popup.gameObject.SetActive(false);
		}

		private bool warned = false;

		protected override void whenEnterRange() {
			base.whenEnterRange();
			popup.gameObject.SetActive(true);

			switch (keyIds.Count) {
				case 0:
					if (!warned) {
						Debug.LogWarning("Object: " + gameObject.name + " in scene: " + gameObject.scene.name + " has no keys");
						warned = true;
					}
					break;
				case 3:
					popup.pop(keyIds[0],keyIds[1],keyIds[2]);
				break;
				case 2:
					popup.pop(keyIds[0], keyIds[1]);
					break;
				case 1:
					popup.pop(keyIds[0]);
					break;
				default:
					int m = keyIds.Count - 3;
					popup.pop(keyIds[0], keyIds[1], keyIds[2], m);
					break;
			}

		}

		protected override void onRangeDisable() {
			popup.gameObject.SetActive(false);
		}

	}
}