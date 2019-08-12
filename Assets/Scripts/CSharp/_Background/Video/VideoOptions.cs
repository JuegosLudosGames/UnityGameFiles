using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.background.video {
	public class VideoOptions : MonoBehaviour {

		public static VideoOptions instance;

		[SerializeField]
		private bool DisplayFps = false;
		public bool displayFps {
			get {
				return DisplayFps;
			}
			set {
				DisplayFps = value;
			}
		}

		[SerializeField]
		private int TargetFrames = 60;
		public int targetFps {
			get {
				return TargetFrames;
			}
			set {
				TargetFrames = value;
			}
		}

		// Start is called before the first frame update
		void Start() {
			instance = this;
		}

	}
}