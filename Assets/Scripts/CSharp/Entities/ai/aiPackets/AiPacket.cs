using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JLG.gift.cSharp.entity.enemy;

	namespace JLG.gift.cSharp.entity.ai.aiPackets {
	public interface AiPacket {
		aistate.AiState GetState(Enemy en);
	}
}