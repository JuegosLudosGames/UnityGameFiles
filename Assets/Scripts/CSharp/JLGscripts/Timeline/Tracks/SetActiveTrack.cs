using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace JLG.gift.cSharp.jglScripts.timeline {
	[TrackColor(1f, 0f, 0f)]
	[TrackBindingType(typeof(ISetActiveInteraction))]
	[TrackClipType(typeof(SetActiveClip))]
	public class SetActiveTrack : TrackAsset {
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount) {
			foreach (var clip in GetClips()) {
				var myAsset = clip.asset as SetActiveClip;
				if (myAsset) {
					myAsset.start = clip.start;
					myAsset.end = clip.end;
				}
			}

			return base.CreateTrackMixer(graph, go, inputCount);
		}
	}
}