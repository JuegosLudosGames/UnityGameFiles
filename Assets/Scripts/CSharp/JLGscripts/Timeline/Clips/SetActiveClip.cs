using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace JLG.gift.cSharp.jglScripts.timeline {
	public class SetActiveClip : PlayableAsset, ITimelineClipAsset {

		public ClipCaps clipCaps { get { return ClipCaps.None; } }

		[SerializeField]
		SetActiveBehaviour template;

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner) {
			return ScriptPlayable<SetActiveBehaviour>.Create(graph, template);
		}
	}
}