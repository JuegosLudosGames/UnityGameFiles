using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.background.scene {
	[System.Serializable]
	public class SceneObjectData {
		public byte state;
		public int objectId;

		public SceneObjectData() {
			state = 0;
			objectId = 0;
		}

		public SceneObjectData(byte state, int id) {
			this.state = state;
			objectId = id;
		}

	}
}