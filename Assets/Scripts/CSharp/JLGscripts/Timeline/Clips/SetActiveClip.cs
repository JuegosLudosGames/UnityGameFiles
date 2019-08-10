using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace JLG.gift.cSharp.jglScripts.timeline {
	public class SetActiveClip : PlayableAsset, ITimelineClipAsset {

		public ClipCaps clipCaps { get { return ClipCaps.None; } }

		[NonSerialized]
		public double start;
		[NonSerialized]
		public double end;

		[SerializeField]
		SetActiveBehaviour template;

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner) {
			//return ScriptPlayable<SetActiveBehaviour>.Create(graph, template);
			var playable = ScriptPlayable<SetActiveBehaviour>.Create(graph, template);
			playable.GetBehaviour().clip = this;

			return playable;
		}
	}
}