using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.background.scene {
	[System.Serializable]
	public struct SceneData {

		public string SceneId;
		public int SavePointId;
		public SceneObjectData[] objectData;

	}
}