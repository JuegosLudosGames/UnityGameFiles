using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.enviroment.triggerable {
	public class Trigger : MonoBehaviour {

		public GameObject[] triggerables;
		public bool triggerOnce = false;

		private bool act = true;

		public void triggerAll() {
			if (act) {
				for (int x = 0; x < triggerables.Length; x++) {
					if (!(triggerables[x].GetComponent<Triggerable>() is null))
						triggerables[x].GetComponent<Triggerable>().onTrigger();
				}
				if (triggerOnce)
					act = false;
			}
		}

	}
}