using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace JLG.gift.cSharp.jglScripts.timeline {
	public abstract class ITimeControlInteract : MonoBehaviour {

		public abstract PlayableDirector getDirector();
		public abstract bool shouldContinueLoop();
	}
}