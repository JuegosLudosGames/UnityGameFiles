using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace JLG.gift.cSharp.jglScripts.timeline {
	[Serializable]
	public class TimelineControlClip : PlayableAsset, ITimelineClipAsset {

		public ClipCaps clipCaps { get { return ClipCaps.None; } }

		[SerializeField]
		TimelineControlBehaviour template = new TimelineControlBehaviour();
		//[NonSerialized]
		//public TimelineClip passthrough = null;
		[NonSerialized]
		public double start;
		[NonSerialized]
		public double end;

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner) {
			//return ScriptPlayable<TimelineControlBehaviour>.Create(graph, template);

			var playable = ScriptPlayable<TimelineControlBehaviour>.Create(graph, template);
			//template.clip = passthrough;
			playable.GetBehaviour().clip = this;

			

			return playable;
		}

	}
}