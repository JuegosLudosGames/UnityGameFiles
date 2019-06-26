using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.entity.player {
	public class FollowPlayer : MonoBehaviour {

		public bool isEnabled = true;
		public bool shouldSmoothing = true;
		[Range(0, 1)]
		public float smoothingSpeed = 0.0f;
		private GameObject player;
		public float zOffset = 0.0f;

		// Start is called before the first frame update
		void Start() {
			player = GameObject.FindGameObjectWithTag("Player");
		}

		// Update is called once per frame
		void FixedUpdate() {
			if (isEnabled) {
				Vector3 moveTo = player.transform.position + new Vector3(0, 0, zOffset);

				if (shouldSmoothing) {
					transform.position = Vector3.Lerp(transform.position, moveTo, smoothingSpeed);
				} else {
					transform.position = moveTo;
				}
			}
		}

	}
}