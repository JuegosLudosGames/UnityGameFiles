using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.jglScripts.timeline {
	public class ISetActiveInteraction : MonoBehaviour {

		public bool startState;

		// Start is called before the first frame update
		void Start() {
			if(!startState)
				setAllActive(false);
		}

		public void setAllActive(bool state) {
			
			Transform[] gb = GetComponentsInChildren<Transform>(true);
			Debug.Log("here with " + gb.Length);

			foreach (Transform g in gb) {
				if (g.gameObject != gameObject) {
					Debug.Log("successful");
					g.gameObject.SetActive(state);
				}
			}
		}

	}
}