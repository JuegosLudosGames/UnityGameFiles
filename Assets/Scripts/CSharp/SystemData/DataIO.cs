using JLG.gift.cSharp.background.input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JLG.gift.cSharp.SystemData {
	[System.Serializable]
	public class DataIO {

		//video settings
		public SerializableKeyInput SerializedkeyBindings;
		public bool displayFPS;
		public int targetFPS;

		//audio settings
		public int MVolume;
		public int SFXVolume;
		public int MuVolume;
		public bool SFXMute;
		public bool MuMute;

	}
}