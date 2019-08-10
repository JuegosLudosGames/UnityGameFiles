using JLG.gift.cSharp.enviroment.triggerable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace JLG.gift.cSharp.jglScripts.timeline {
	[TrackColor(76f/255f,44f/255f,171f/255f)]
	[TrackBindingType(typeof(ITimeControlInteract))]
	[TrackClipType(typeof(TimelineControlClip))]
	public class TimelineControlTrack : TrackAsset {

		//public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount) {
		//	var clips = GetClips();
		//	foreach (var clip in clips) {
		//		var tclip = clip.asset as TimelineControlClip;
		//		tclip.passthrough = clip;
		//	}

		//}

		//protected override void OnCreateClip(TimelineClip clip) {
		//	base.OnCreateClip(clip);

		//	var tclip = clip.asset as TimelineControlClip;
		//	//tclip.passthrough = clip;

		//}

		//protected override Playable CreatePlayable(PlayableGraph graph, GameObject gameObject, TimelineClip clip) {

		//	var tclip = clip.asset as TimelineControlClip;
		//	tclip.passthrough = clip;

		//	return base.CreatePlayable(graph, gameObject, clip);
		//}

		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount) {
			foreach (var clip in GetClips()) {
				var myAsset = clip.asset as TimelineControlClip;
				if (myAsset) {
					myAsset.start = clip.start;
					myAsset.end = clip.end;
				}
			}

			return base.CreateTrackMixer(graph, go, inputCount);
		}


	}
}