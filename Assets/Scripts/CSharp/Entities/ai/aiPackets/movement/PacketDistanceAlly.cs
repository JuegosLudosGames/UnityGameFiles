﻿using System.Collections;
using System.Collections.Generic;
using JLG.gift.cSharp.entity.ai.aistate;
using JLG.gift.cSharp.entity.enemy;
using UnityEngine;

namespace JLG.gift.cSharp.entity.ai.aiPackets {
	public class PacketDistanceAlly : AiPacket {

		//the min distance an enemy has to be from other enemies
		float minD;

		//the constructor with distance
		public PacketDistanceAlly(float minDistance) =>
			minD = minDistance;

		//gets state based on enemy 
		public AiState GetState(Enemy en) {
			//inits state variable
			AiState state = null;

			//gets the closest enemy that is withing the min distance
			Enemy ce = en.getClosestFriendInRange(minD);

			//if there is an enemy too close
			if (ce != null) {
				//run in opposite direction
				state = new StateWalkTo(-ce.transform.position);
			}

			return state;
		}
	}
}