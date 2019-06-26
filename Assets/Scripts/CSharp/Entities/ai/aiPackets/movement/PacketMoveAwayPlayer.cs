using System.Collections;
using System.Collections.Generic;
using JLG.gift.cSharp.entity.ai.aistate;
using JLG.gift.cSharp.entity.enemy;
using UnityEngine;

namespace JLG.gift.cSharp.entity.ai.aiPackets {
	public class PacketMoveAwayPlayer : AiPacket {

		//min distance from player
		private float min;

		//constructor with distance
		public PacketMoveAwayPlayer(float minDistance) =>
			(this.min) = minDistance;

		//gets state based on enemy
		public AiState GetState(Enemy en) {
			//inits state
			AiState state = null;

			//checks if enemy was recently damaged (run away after damage)
			//checks if too close
			if (en.wasDamaged || Vector3.Distance(en.transform.position, Entity.player.transform.position) < min) {
				//returns state to walk in opposite direction
				state = new StateWalkTo(reflectAcrossVerticalPlane(Entity.player.transform.position, en.transform.position));
			}
			return state;
		}

		//reflects a point accross a vertical plane
		private Vector3 reflectAcrossVerticalPlane(Vector3 point, Vector3 planeLoc) {
			//float newX = planeLoc.x - (point.x - planeLoc.x);
			float newX = (point.x < planeLoc.x) ? min : -min;
			return new Vector3(newX, planeLoc.y, planeLoc.z);
		}

	}
}