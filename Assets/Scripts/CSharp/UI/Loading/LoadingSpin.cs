using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.ui.loading {
	public class LoadingSpin : MonoBehaviour {

		public float speed;

		// Update is called once per frame
		void Update() {
			transform.Rotate(new Vector3(0,0,speed));
		}
	}
}