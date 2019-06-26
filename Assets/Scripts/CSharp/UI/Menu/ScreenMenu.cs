using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.ui.menu {
	public class ScreenMenu : MonoBehaviour {

		protected GameObject active {
			get; private set;
		}

		public void sendTo(GameObject current, GameObject sendTo) {
			if (current != null) {
				current.gameObject.SetActive(false);
			}
			if (sendTo != null) {
				sendTo.gameObject.SetActive(true);
				active = sendTo;
			} else {
				active = null;
			}
		}

		public void setActive(GameObject obj, bool state) {
			obj.SetActive(state);
			if (state)
				active = obj;
		}

		//public void setNoneActive() {
		//	active.SetActive(false);
		//	active = null;
		//}

	}
}