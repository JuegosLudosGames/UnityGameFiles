using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JLG.gift.cSharp.entity.enemy;
using JLG.gift.cSharp.entity.ai.aiPackets;
using JLG.gift.cSharp.attackData.spells.spelltype;

namespace JLG.gift.cSharp.entity.enemy {

	public class EasyStrength : Enemy {

		//public variables
		[Header("Control")]
		[Header("	Derived from EasyStrength")]
		public float WanderDistance = 5.0f;
		public float enemyDistance = 1.5f;
		public float minPlayerDistance = 1.5f;
		public float minPlayerDistanceError = 0.1f;
		//public float retreatCoolDown = 3.0f;

		[Header("First Magic Attack")]
		public Spell firstM;

		//protected override void onEnemyStart() {
			
		//	//add all packets
		//	packets.Add(new PacketMoveAwayPlayer(minPlayerDistance - minPlayerDistanceError));
			
		//	packets.Add(new PacketDistanceAlly(enemyDistance));
		//	packets.Add(new PacketAttackInRange(slashAttackRange));
			
		//	packets.Add(new PacketShootInRange(firstM, 1));
		//	packets.Add(new PacketMoveToPlayer(minPlayerDistance + minPlayerDistanceError));
		//	packets.Add(new PacketRoam(WanderDistance));

		//}

	}
}