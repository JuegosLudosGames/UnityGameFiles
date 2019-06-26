using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.enviroment.triggerable {
	public class DistanceTrigger : Trigger {

		public float triggerDistance;

		private bool act2 = true;

		void Update() {
			if (act2) {
				if (isPlayerInRange()) {
					act2 = !triggerOnce;
					triggerAll();
				}
			}
		}

		private bool isPlayerInRange() {
			return Vector3.Distance(entity.Entity.player.transform.position, transform.position) <= triggerDistance;
		}

	}
}