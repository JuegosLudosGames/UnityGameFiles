using System.Collections;
using System.Collections.Generic;
using JLG.gift.cSharp.entity.ai.aistate;
using JLG.gift.cSharp.entity.enemy;
using UnityEngine;

namespace JLG.gift.cSharp.entity.ai.aiPackets {
	public class PacketMoveToPlayer : AiPacket {

		//min distance enemy should be from player
		private float min;

		//constructor with min distance
		public PacketMoveToPlayer(float min) {
			this.min = min;
		}

		//get state using enemy object
		public AiState GetState(Enemy en) {
			//inits state variable
			AiState state = null;

			//checks if within distance
			if (Mathf.Abs(en.transform.position.x - Entity.player.transform.position.x) > min) {
				//is the player spoted
				if (en.playerSpotted) {
					//if spoted, walk to player position
					state = new StateWalkTo(Entity.player.transform.position);
				} else if (en.playerWasSpotted) {
					//if cannot see, walk to last seen location
					state = new StateWalkTo(en.lastSpotting);
				}
			}

			//gives state
			return state;
		}
	}
}