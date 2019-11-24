using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.jglScripts.objectComponents {
	public class SelfDestructableObject : MonoBehaviour {

		public float awakeLifeTime;
		public bool startOnAwake = false;

		private float TimeLeft = 0.0f;
		private bool started = false;

		private void Awake() {
			if (startOnAwake) {
				startCountDown();
			}
		}

		// Update is called once per frame
		void Update() {
			if (started) {
				if (TimeLeft <= 0) {
					GameObject.Destroy(gameObject);
				} else {
					TimeLeft -= Time.deltaTime;
				}
			}
		}

		public void startCountDown() {
			TimeLeft = awakeLifeTime;
			started = true;
		}

		public void startCountDown(float time) {
			TimeLeft = time;
			started = true;
		}

	}
}