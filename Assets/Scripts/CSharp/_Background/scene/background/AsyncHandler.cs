using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.background {
	public class AsyncHandler : MonoBehaviour {

		public static AsyncHandler instance;

		// Start is called before the first frame update
		void Start() {
			instance = this;
		}

		public void startAsyncTask(IEnumerator method) {
			StartCoroutine(method);
		}

	}
}