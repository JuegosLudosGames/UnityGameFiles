using JLG.gift.cSharp.enviroment.triggerable;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace JLG.gift.cSharp.jglScripts.timeline {
	[Serializable]
	public class TimelineControlBehaviour : PlayableBehaviour {

		public static readonly double wiggle = 0.05;

		public bool shouldFreeze;
		public bool shouldJump;

		[HideInInspector]
		public TimelineControlClip clip;

		//[NonSerialized]
		//public TimelineClip clip;
		//[HideInInspector]
		//public TimelineControlClip clip;

		public override void ProcessFrame(Playable playable, FrameData info, object playerData) {

			var preScene = playerData as ITimeControlInteract;

			if (preScene == null) {
				Debug.LogError("null 1");
				return;
			}

			if (clip == null) {
				Debug.LogError("null 2");
			}

			PlayableDirector dir = preScene.getDirector();

			if (dir == null) {
				Debug.LogError("null 3");
				return;
			}

			Debug.Log("current " + dir.time);
			Debug.Log("end " + clip.end);

			//test if on last frame
			if (dir.time >= (clip.end - wiggle) && dir.time <= clip.end) {
				Debug.LogWarning("Last");
				//are we freeze
				if (shouldFreeze && preScene.shouldContinueLoop()) {
					dir.Pause();
				} else if (shouldJump && preScene.shouldContinueLoop()) {
					dir.time = clip.start;
				}
			}

		}

	}
}