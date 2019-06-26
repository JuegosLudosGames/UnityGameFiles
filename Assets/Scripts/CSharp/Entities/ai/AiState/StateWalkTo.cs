using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.entity.ai.aistate {
	public class StateWalkTo : AiState {

		//position to walk towards
		public Vector3 pos {
			get { return posI; }
		}
		private Vector3 posI;

		//constuctor
		public StateWalkTo(Vector3 pos) {
			this.posI = pos;
		}
	}
}