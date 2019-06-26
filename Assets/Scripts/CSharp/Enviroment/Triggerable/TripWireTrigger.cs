using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.enviroment.triggerable {
	[RequireComponent(typeof(EdgeCollider2D))]
	public class TripWireTrigger : Trigger {

		EdgeCollider2D col;

		// Start is called before the first frame update
		void Start() {
			col = GetComponent<EdgeCollider2D>();
		}

		private void OnCollisionEnter(Collision collision) {
			triggerAll();
		}

	}
}