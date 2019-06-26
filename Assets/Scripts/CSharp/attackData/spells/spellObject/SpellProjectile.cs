using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JLG.gift.cSharp.attackData.spells.spelltype;
using JLG.gift.cSharp.entity;


namespace JLG.gift.cSharp.attackData.spells.spellObject {

	[RequireComponent(typeof(CircleCollider2D))]
	public class SpellProjectile : MonoBehaviour {

		public Spell type;		//the type of spell
		public float absAdd;	//damage to be added when attacking players
		public float multi;		//multiplier to be used when attacking players

		private float timeLeft = 0.0f;							//time before destruction of gameobject
		private List<Entity> attacked = new List<Entity>();		//all entities that was previously attacked by this projectile
		private bool fromPlayer;								//is this projectile from the player
		private CircleCollider2D range;							//collider2d acting as attack range
		private bool right = true;								//direction moving, to the right?

		//for use of the spell controller
		//sets info of the projectile
		public void setInfo(bool player, bool Right, Entity origin, float add, float multi) {
			(fromPlayer, right, absAdd, this.multi) = (player, Right, add, multi);
			attacked.Add(origin);
		}

		//starts the projectile actions
		public void startP() {
			timeLeft = type.LifeTime;
			range = GetComponent<CircleCollider2D>();
			range.radius = type.Range;
		}

		// Update is called once per frame
		void Update() {
			//check the projectiles life
			timeLeft -= Time.deltaTime;
			if (timeLeft <= 0) {
				//if life is over, destroy
				GameObject.Destroy(this.gameObject);
				return;
			}

			//moves projectile
			transform.position += (right ? Vector3.right : Vector3.left).normalized * (Time.deltaTime * type.Speed);

			//checks to damage either entity or player
			if (fromPlayer) {
				attackRange();
			} else {
				attackPlayer();
			}
		}

		//attacks all non-attacked entities
		private void attackRange() {
			//gets checks every entity that can be attacked
			foreach (Entity en in getAttackableEntities(range)) {
				//damages entity
				en.damage((type.Damage + absAdd)*multi);
				//can the projectile attack more than 1 entity
				if (type.SingleOnly) {
					//if so, break gameobject
					GameObject.Destroy(this.gameObject);
				}
			}
			
		}

		//attacks the player if in range
		private void attackPlayer() {
			
			//check if can attack player
			if (isPlayerInRange(range) && !attacked.Contains(Entity.player)) {
				//if so, damages player
				Entity.player.damage((type.Damage + absAdd)*multi);
				attacked.Add(Entity.player);
				//if it can attack more than once,
				if (type.SingleOnly) {
					//if so, break gameobject
					GameObject.Destroy(this.gameObject);
				}
			}
			
		}

		//maximum number of colliders to check 
		private int maxColliderTest = 20;

		private List<Entity> getAttackableEntities(Collider2D range) {
			//creates output list
			List<Entity> entites = new List<Entity>();

			//creates filter
			ContactFilter2D filter = new ContactFilter2D();
			//filter.SetLayerMask(10); //filter only attackable enemies
			filter.useTriggers = true; //filter for triggers

			//holds results of method
			Collider2D[] results = new Collider2D[maxColliderTest];
			//gets all overlapping colliders
			int suc = range.OverlapCollider(filter, results);

			//goes through every result
			foreach (Collider2D test in results) {
				//if reached the end of teh list or error
				if (test == null)
					continue;
				//is this a hitbox?
				if (test.gameObject.GetComponentInParent<Transform>().GetComponentInParent<Entity>() != null) {
					if (test == test.gameObject.GetComponentInParent<Transform>().GetComponentInParent<Entity>().hitBox && test.gameObject.GetComponentInParent<Transform>().GetComponentInParent<Entity>() != this) {
						//check if not already attacked
						if (!attacked.Contains(test.gameObject.GetComponentInParent<Transform>().GetComponentInParent<Entity>())) {
							//add to list
							entites.Add(test.gameObject.GetComponentInParent<Transform>().GetComponentInParent<Entity>());
							attacked.Add(test.gameObject.GetComponentInParent<Transform>().GetComponentInParent<Entity>());
						}
					}
				}
			}

			//return list
			return entites;
		}

		//checks if player can be attacked
		private bool isPlayerInRange(Collider2D range) {
			//Debug.Log("checking");
			//creates filter
			ContactFilter2D filter = new ContactFilter2D();
			//filter.SetLayerMask(1 << 10); //filter only attackable enemies
			filter.layerMask = 1 << 12;
			filter.useTriggers = true; //filter for triggers

			//holds results of method
			Collider2D[] results = new Collider2D[maxColliderTest];
			//gets all overlapping colliders
			int suc = range.OverlapCollider(filter, results);
			//Debug.Log(results);

			//goes through every result
			foreach (Collider2D test in results) {
				//if reached the end of teh list or error
				if (test == null)
					continue;
				//is this a hitbox of the player?
				if (test == Entity.player.hitBox) {
					//return true
					return true;
				}
			}
			return false;
		}

	}
}