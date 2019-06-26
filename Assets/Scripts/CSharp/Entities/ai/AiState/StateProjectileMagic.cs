using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.entity.ai.aistate {
	public class StateProjectileMagic : MonoBehaviour {

		//vector angle for directin to shoot
		public Vector2 angle {
			get { return angleI; }
		}
		private Vector2 angleI;

		//constructor with angel
		public StateProjectileMagic(Vector2 angle) {
			angleI = angle;
		}
	}
}