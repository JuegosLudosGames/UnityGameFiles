using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

namespace JLG.gift.cSharp.jglScripts.timeline {
	[TrackColor(1f, 0f, 0f)]
	[TrackBindingType(typeof(ISetActiveInteraction))]
	[TrackClipType(typeof(SetActiveClip))]
	public class SetActiveTrack : TrackAsset {
		
	}
}