using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JLG.gift.cSharp.background.scene {
	public class ISceneObjectData : MonoBehaviour {
		//SceneObjectData stateData { get; set; }
		public SceneObjectData stateData;
		public UnityEvent onDataLoad;
	}
}