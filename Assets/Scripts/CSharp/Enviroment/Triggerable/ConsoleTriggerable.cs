using JLG.gift.cSharp.background.scene.background;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.enviroment.triggerable {
	public class ConsoleTriggerable : MonoBehaviour, Triggerable {
		public void onTrigger() {
			MessageBox.instance.messageInfo("WORKING");
		}

		// Start is called before the first frame update
		void Start() {

		}

		// Update is called once per frame
		void Update() {

		}
	}
}