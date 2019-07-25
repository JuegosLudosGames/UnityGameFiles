using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.enviroment.triggerable {
	public class ManualTrigger : Trigger {

		public bool trigger = false;

		private void Update() {
			if (trigger) {
				trigger = false;
				triggerAll();
			}
		}

	}
}