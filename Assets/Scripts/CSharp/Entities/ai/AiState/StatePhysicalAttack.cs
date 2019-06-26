using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.entity.ai.aistate {
	public class StatePhysicalAttack : AiState {

		//boolean if attacking to the right
		public bool toRight {
			get { return toRightI; }
		}
		private bool toRightI;

		//constructor with direction
		public StatePhysicalAttack(bool toRight) {
			toRightI = toRight;
		}
	}
}