using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace JLG.gift.cSharp.jglScripts.timeline {
	public class ISetActiveInteraction : MonoBehaviour {

		public bool startState;
		public PlayableDirector dir;

		// Start is called before the first frame update
		void Start() {
			if(!startState)
				setAllActive(false);
		}

		public void setAllActive(bool state) {
			
			Transform[] gb = GetComponentsInChildren<Transform>(true);

			foreach (Transform g in gb) {
				if (g.gameObject != gameObject) {
					g.gameObject.SetActive(state);
				}
			}
		}

	}
}