using System.Collections;
using System.Collections.Generic;
using JLG.gift.cSharp.attackData.spells.spelltype;
using JLG.gift.cSharp.entity.ai.aistate;
using JLG.gift.cSharp.entity.enemy;
using UnityEngine;

namespace JLG.gift.cSharp.entity.ai.aiPackets {
	public class PacketShootInRange : AiPacket {

		//spell that packet is set for
		private Spell spell;
		//the distance range for the spell
		private float range;
		//number identifyier of the spell
		private int num;

		//constructor for spell and num identifier
		public PacketShootInRange(Spell spell, int num) {
			(this.spell, this.num) = (spell, num);
			range = spell.LifeTime * spell.Speed;
		}

		//gets state based on enemy
		public AiState GetState(Enemy en) {
			//inits state
			AiState state = null;
			//can enemy see player
			if (en.playerSpotted) {
				//is the enemy able to shoot
				if (en.shouldShoot(spell, num)) {
					//is the player in range
					if (inRange(en.transform.position)) {
						//gets state for firing
						state = new StateStraightMagic(spell, num);
					}
				}
			}
			//returns the state
			return state;
		}

		//checks is player is in range of the spell
		private bool inRange(Vector3 pos) {
			if (Vector3.Distance(pos, Entity.player.transform.position) <= range)
				return true;
			return false;
		}
	}
}