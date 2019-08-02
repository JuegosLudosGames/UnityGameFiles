using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace JLG.gift.cSharp.jglScripts.timeline {
	[Serializable]
	public class SetActiveBehaviour : PlayableBehaviour {

		private static readonly double wiggle = 0.05;

		public bool onlyChangeStateOnce = false;
		public bool setEnableState = true;
		bool wasrun = false;

		[HideInInspector]
		public SetActiveClip clip;

		public override void ProcessFrame(Playable playable, FrameData info, object playerData) {

			//is this a single use change
			if (onlyChangeStateOnce) {
				//single use
				//did it already complete the action
				if (!wasrun) {
					//get interaction object
					var interaction = playerData as ISetActiveInteraction;

					if (interaction == null) {
						Debug.LogError("Interaction null");
						return;
					}

					//set to desired state
					interaction.setAllActive(setEnableState);
					//stop running
					wasrun = true;
				}

			} else {
				//multiple uses

				//get interaction object
				var interaction = playerData as ISetActiveInteraction;

				if (interaction == null) {
					Debug.LogError("Interaction null");
					return;
				}

				//get director
				PlayableDirector dir = interaction.dir;

				if (dir == null) {
					Debug.LogError("Director Null");
					return;
				}

				//was it enabled
				if (!wasrun) {
					//not yet
					//set to true
					interaction.setAllActive(true);
					wasrun = true;
				//it is enabled, is it in time
				} else if(dir.time >= (clip.end - wiggle) && dir.time <= clip.end) {
					//set to false
					interaction.setAllActive(false);
				}
			}
		}
	}
}