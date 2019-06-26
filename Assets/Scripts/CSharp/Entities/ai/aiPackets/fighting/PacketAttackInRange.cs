using System.Collections;
using System.Collections.Generic;
using JLG.gift.cSharp.entity.ai.aistate;
using JLG.gift.cSharp.entity.enemy;
using UnityEngine;

namespace JLG.gift.cSharp.entity.ai.aiPackets {
	public class PacketAttackInRange : AiPacket {

		//the attack range for the packet
		Collider2D range;

		//constructing the packet with range
		public PacketAttackInRange(Collider2D range) {
			this.range = range;
		}

		//returns a state using enemy data
		public AiState GetState(Enemy en) {
			//inits the variable
			AiState state = null;

			//is the enemy able to attack
			if (en.shouldAttack()) {
				//is the player in range to attack
				if (isPlayerInRange()) {
					//gives attack state
					state = new StatePhysicalAttack(en.isLookingInDirection());
				}
			}

			//return the state
			return state;
		}

		//checks if player is within range of collider
		private bool isPlayerInRange() {

			//creates collider array
			Collider2D[] r = new Collider2D[20];

			//creates filter for search
			ContactFilter2D filter = new ContactFilter2D();
			filter.useLayerMask = true;
			filter.layerMask = 1 << 12;
			filter.useTriggers = true;

			//gets objects
			range.OverlapCollider(filter, r);

			//checks each collider
			foreach (Collider2D c in r) {
				//is it player
				if (c == Entity.player.hitBox) {
					//return true
					return true;
				}
			}
			//return false if player not found
			return false;

		}

	}
}