using System.Collections;
using System.Collections.Generic;
using JLG.gift.cSharp.entity.ai.aistate;
using JLG.gift.cSharp.entity.enemy;
using UnityEngine;

namespace JLG.gift.cSharp.entity.ai.aiPackets {
	public class PacketRoam : AiPacket {

		//the wander distance
		private float wander;
		//is the previous direction determined to the right
		private bool isRightPrev = true;

		//constructor with roam distance
		public PacketRoam(float wander) {
			this.wander = wander;
		}

		//gets state based on enemy 
		public AiState GetState(Enemy en) {
			//inits state
			AiState state = null;

			//checks if the distance from spawnpoint is withing wander range
			if (Vector3.Distance(en.spawnPoint, en.transform.position) >= wander) {
				//yes we are too far
				//sets state to walk towards spawnpoint
				state = new StateWalkTo(new Vector3 (en.spawnPoint.x, en.transform.position.y));
				//sets previous direction towards spawnpoint
				isRightPrev = en.spawnPoint.x >= en.transform.position.x;
			} else {
				//no we are not too far
				//checks if it wants to go right (1 in 25 chance)
				bool goRight = (Random.Range(0, 1) * 25) == 1 ? !isRightPrev : isRightPrev;

				////creates layer mask for overlap check
				//int layerMask = (1 << 9) | (1 << 11);
				//bool Wallpresent = false; //bool value if a wall is in front

				////check if should jump
				//if(Physics2D.OverlapCircle(new Vector2(en.transform.position.x + (goRight ? 0.8f : -0.8f), en.transform.position.y), 1,layerMask) != null) {
				//	//check if unable bc too tall if too tall, do not jump over it, otherwise jump
				//	if (Physics2D.OverlapCircle(new Vector2(en.transform.position.x + (goRight ? 0.8f : -0.8f), en.transform.position.y + 2f), 1, layerMask) != null) {
				//		goRight = !goRight;
				//	} else {
				//		Wallpresent = true;
				//	}
				//}

				////determines direction or jump area
				//Vector3 to = new Vector3(Wallpresent ? 0 : en.transform.position.x + (goRight ? 0.5f : -0.5f), Wallpresent ? en.transform.position.y + 5 : en.transform.position.y);
				////sets state

				Vector3 to = new Vector3(en.transform.position.x + (goRight ? wander : -wander), en.transform.position.y);

				state = new StateWalkTo(to);
				//sets previous direction variable
				isRightPrev = goRight;
			}
			//returns state
			return state;
		}
	}
}