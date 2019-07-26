using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace JLG.gift.cSharp.jglScripts.timeline {
	[Serializable]
	public class SetActiveBehaviour : PlayableBehaviour {

		public bool setEnableState = true;
		bool wasrun = false;

		public override void ProcessFrame(Playable playable, FrameData info, object playerData) {

			if (!wasrun) {
				var interaction = playerData as ISetActiveInteraction;

				if (interaction == null) {
					Debug.LogError("Interaction null");
					return;
				}

				Debug.Log("run");
				interaction.setAllActive(setEnableState);
				wasrun = true;
			}
		}
	}
}