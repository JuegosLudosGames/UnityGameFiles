using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JLG.gift.cSharp.enviroment.interactble;
using JLG.gift.cSharp.background.scene.background;

namespace JLG.gift.cSharp.enviroment.intertriggerables {
	public class MessageTester : Interactable {

		[TextArea]
		public string test;

		protected override void onStart() {
			base.onStart();
			rangeEnable = true;
		}

		protected override void onInteract() {
			MessageBox.instance.messageInfo(test);
		}
	}
}