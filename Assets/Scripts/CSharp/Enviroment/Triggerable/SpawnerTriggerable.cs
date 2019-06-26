using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JLG.gift.cSharp.entity.enemy;
using JLG.gift.cSharp.attackData.spells.spelltype;
using JLG.gift.cSharp.background.scene;
using JLG.gift.cSharp.background.scene.background;

namespace JLG.gift.cSharp.enviroment.triggerable {
	public class SpawnerTriggerable : MonoBehaviour, Triggerable {

		public float range;
		public SpawnSettings[] spawns;

		public void onTrigger() {
			//spawn each type
			GameObject fold = SceneController.currentScene.EnemyFolder;
			GameObject pre = GlobalItems.instance.EnemyInstancePrefab;
			foreach (SpawnSettings sp in spawns) {

				//spawn temp the enemy
				Enemy etemp = GameObject.Instantiate(pre, getRangePos(), pre.transform.rotation, fold.transform).GetComponent<Enemy>();
				etemp.persona = sp.persona;
				etemp.magic1 = sp.magic1;
				etemp.magic2 = sp.magic2;
				etemp.baseDifficulty = sp.difficulty;
				GameObject.Instantiate(sp.spriteModel, etemp.transform);

				//spawn number of declared enemies
				for (int x = 0; x < sp.amount; x++) {
					//spawn the enemy
					Enemy e = GameObject.Instantiate(etemp, getRangePos(), pre.transform.rotation, fold.transform).GetComponent<Enemy>();
				}

				GameObject.Destroy(etemp.gameObject);

			}
		}

		public Vector3 getRangePos() {
			Vector3 pos = transform.position;
			return new Vector3(Random.Range(-range, range) + pos.x, pos.y, pos.z);
		}

		[System.Serializable]
		public class SpawnSettings {
			public Enemy.Personality.Personalities persona;
			public Spell magic1;
			public Spell magic2;
			public int difficulty;
			[Range(1,10)]
			public int amount = 1;
			public GameObject spriteModel;
		}


	}
}