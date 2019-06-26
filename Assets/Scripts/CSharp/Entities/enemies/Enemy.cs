using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JLG.gift.cSharp.entity.ai.aiPackets;
using JLG.gift.cSharp.entity.ai.aistate;
using JLG.gift.cSharp.attackData.spells.spelltype;
using JLG.gift.cSharp.inventory;
using static JLG.gift.cSharp.entity.enemy.Enemy.Personality;

namespace JLG.gift.cSharp.entity.enemy {

	public class Enemy : Entity {
		
		//public variables
		[Header("Control")]
		[Header("	Derived from Enemy")]
		public float fallMultiplier = 2.5f;
		public float baseDifficulty = 1;
		public float aiDelay = 1;
		[HideInInspector]
		public float forgetTime = 20;
		public float viewRange = 10;
		public float viewAngle = 50.0f;
		[HideInInspector]
		public float damageReactionCooldown = 3.0f;
		[HideInInspector]
		public float attackReactionCooldown = 1.5f;
		public float gapCheck = 1.5f;

		[Header("Basic Attack")]
		public float slashAttackDamage = 0.0f;
		public Collider2D slashAttackRange;
		public float slashAttackTime = 0.0f;
		public Spell magic1;
		public Spell magic2;
		public Personalities persona;

		//used variables
		protected List<AiPacket> packets = new List<AiPacket>();
		public Vector3 spawnPoint {
			get; private set;
		}
		public bool playerSpotted {
			get; private set;
		}
		public bool playerWasSpotted {
			get; private set;
		}
		public Vector3 lastSpotting {
			get; private set;
		}
		[HideInInspector]
		public float timeToForget = 0.0f;
		public bool wasDamaged {
			get; private set;
		}
		[HideInInspector]
		public float damagedCool = 0.0f;
		private ItemDropper dropper;
		

		protected override void onStart() {
			//base.onAwake();

			//gets all required components
			dropper = GetComponent<ItemDropper>();

			//spoint = Vector3.zero;
			//sets the spawnpoint to large number before setting
			spawnPoint = new Vector3(10000, 10000, 10000);
			
			//checks if no delay will be used, if so sets spawnpoint
			if (aiDelay <= 0)
				this.spawnPoint = transform.position;

			//get personality
			packets = Personality.getPacketsFromPersona(persona, this);
			object[] o = Personality.getSettingsFromPersona(persona);
			forgetTime = (float) o[0];
			damageReactionCooldown = (float) o[1];
			attackReactionCooldown = (float) o[2];

			//calls enemy start sequence
			//onEnemyStart();
		}

		protected override void onEarlyUpdate() {
			//does early update of Entity
			base.onEarlyUpdate();


			//check for spawnpoint
			if (spawnPoint == new Vector3(10000,10000,10000)) {
				//delay is gone
				if (aiDelay <= 0) {
					//set spawnpoint
					this.spawnPoint = transform.position;
				} else {
					//if not, subtract time
					aiDelay -= Time.deltaTime;
				}
				//stops enemy freezing Ai
				return;
			}

			//was the player spotted and is tracking
			if (playerWasSpotted) {
				//subtracts time
				timeToForget -= Time.deltaTime;

				//did enemy forget
				if (timeToForget <= 0) {
					//forget player
					playerSpotted = false;
					playerWasSpotted = false;
				}

			}

			//looks around for player
			checkForPlayer();

			//look in current direction if only player is currently in view
			continueLookingdir = playerSpotted;

			//damage state timer
			if (wasDamaged) {
				//if damaged, go through cooldown
				damagedCool -= Time.deltaTime;
				//stop damage reaction?
				if (damagedCool <= 0)
					wasDamaged = false;
			}

			//gets wanted state
			AiState state = getAiState();

			//checks states
			if (state is StateWalkTo) {
				//its a walking state, get object
				StateWalkTo s = (StateWalkTo)state;

				//gets relative Vector of point
				Vector3 relative = s.pos - transform.position;

				//should we jump? High relative position and a jumpable wall or gab present
				if (relative.y > 0.2 || isGapOrWallPresent()) {
					//jump
					jump(jumpStrength);
				}
				//should go right
				if (relative.x > 0) {
					//move to right with speed
					movex(speed);
				} else if (relative.x < 0) {
					//otherwise move left with speed
					movex(-speed);
				}
			} else {
				//if not walking state, walk with 0 speed
				movex(0);
			}

			//are we attacking?
			if (state is StatePhysicalAttack) {
				//we are attacking, get object
				StatePhysicalAttack s = (StatePhysicalAttack)state;
				//is enemy able to attack
				if (shouldAttack()) {
					//attack
					attackPlayer(slashAttackRange, slashAttackDamage, slashAttackTime);
					//trigger animation for attack
					modelAnimator.SetTrigger("Attack");
					//Debug.Log("Attacking");
				}
			}

			//should attack with magic
			if (state is StateStraightMagic) {
				//get object
				StateStraightMagic s = (StateStraightMagic)state;
				//shoot
				shoot(s.spell, s.num, 0, 1);
				//trigger animation
				modelAnimator.SetTrigger("Magic");
			}

		}

		//checks for gap or wall in front
		private bool isGapOrWallPresent() {

			//gets check position
			Vector3 posc = new Vector3(transform.position.x + (isLookingInDirection() ? gapCheck : -gapCheck), transform.position.y);

			//collider overlap check
			Collider2D o = Physics2D.OverlapCircle(posc, 0.1f);

			//if no wall found
			if (o != null) {
				//checks layer
				if (o.gameObject.layer == 11 || o.gameObject.layer == 9) {
					return true;
				}
			}

			//checks if gap
			//gets check position
			posc = new Vector3(this.transform.position.x + (isLookingInDirection()? gapCheck : -gapCheck), this.transform.position.y - 1.5f);

			//collider overlap check
			o = Physics2D.OverlapCircle(posc, 0.3f);
			//is no floor found
			if (o == null) {
				return true;
			}
			return false;
		}

		protected override void onLateUpdate() {
			base.onLateUpdate();

			//fall drag incrementing
			if (rb.velocity.y < 0) {
				rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
			}
		}

		//abstract for when enemy starts
		//protected abstract void onEnemyStart();

		//gets state of ai
		public AiState getAiState() {

			//goes through all packs in order
			foreach (AiPacket pack in packets) {

				//gets the state using current instance
				AiState state = pack.GetState(this);

				//is no state returned
				if (state != null) {
					//return state
					return state;
				}
			}
			//otherwise dont do anything
			return null;
		}

		//damages entity
		public override void damage(float damage) {
			//does base damage for entity
			base.damage(damage);

			//sets damaged retreat reactiong
			wasDamaged = true;
			//sets cooldown
			damagedCool = damageReactionCooldown;
			//says player is spoted from damage
			PlayerSpotted();
		}

		//attacks the player
		protected override void attackPlayer(Collider2D range, float damage, float time) {
			//does entity attack
			base.attackPlayer(range, damage, time);

			//sets damage reaction state
			wasDamaged = true;

			//sets cooldown
			damagedCool = attackReactionCooldown;
		}

		protected override void death() {
			base.death();
			if (!(dropper is null)) {
				dropper.drop();
			}
		}

		protected void PlayerSpotted() {
			playerSpotted = true;
			playerWasSpotted = true;
			timeToForget = forgetTime;
			lastSpotting = Entity.player.transform.position;
		}

		public Enemy getClosestFriendInRange(float range) {
			//get all enemies
			//create filter
			ContactFilter2D filter = new ContactFilter2D();
			filter.layerMask = 1 << 10;
			filter.useTriggers = true;
			Collider2D[] hits = new Collider2D[20];
			int tests = Physics2D.OverlapCircle(transform.position, range, filter, hits);

			if (tests > 0) {

				Enemy max = null;
				float distance = Mathf.Infinity;

				foreach (Collider2D h in hits) {

					if (h == null) {
						continue;
					}

					//find EnemyComponent
					if (h.GetComponentInParent<Transform>() != null) {
						if (h.GetComponentInParent<Transform>().GetComponentInParent<Transform>() != null) {
							Enemy e = h.GetComponentInParent<Transform>().GetComponentInParent<Transform>().GetComponent<Enemy>();
							//check if possible hitbox and his enemy
							if (e != null) {
								//check if not self
								if (e == this) {
									continue;
								}
								//check if closest
								float cd = Vector3.Distance(transform.position, e.transform.position);
								if (cd < distance) {
									//set max
									max = e;
									distance = cd;
								
								} 
							} 
						}
					}

				}
				return max;
			} else {
				return null;
			}
			
		}

		//checks if player is withing entities range of view
		private void checkForPlayer() {
			//is player within distance?
			if (Vector3.Distance(Entity.player.transform.position, transform.position) <= viewRange) {
				//is player on the correct side of entity
				if (isLookingInDirection(Entity.player.transform.position)) {
					//is player within angle
					if (Vector3.Angle(Entity.player.transform.position - transform.position, isLookingInDirection() ? Vector3.right : Vector3.left) <= viewAngle) {
						//is being blocked
						if(!Physics2D.Linecast(transform.position, Entity.player.transform.position, (1 << 9) | (1 << 11))) { 
							PlayerSpotted();
							return;
						} 
					} 
				}
			} 
			playerSpotted = false;
		}

		public class Personality {

			public enum Personalities {
				COWARD,
				RUSHER,
				NEUTRAL
			}

			public static Personality[] personas = new Personality[] {
				/*Coward*/ new Personality(
							   new System.Type[] { typeof(PacketMoveAwayPlayer), typeof(PacketDistanceAlly), typeof(PacketAttackInRange),
								   typeof(PacketShootInRange), typeof(PacketMoveToPlayer), typeof(PacketRoam)}, 
							   new object[] {1.4f, 1.5f, null, null, 1, 1.6f, 5.0f}, 
							   new object[] {10.0f, 3.0f, 2.0f }
							   ),
				/*Rusher*/ new Personality(
							   new System.Type[] { typeof(PacketMoveAwayPlayer), typeof(PacketDistanceAlly), typeof(PacketAttackInRange),
								   typeof(PacketMoveToPlayer), typeof(PacketRoam)},
							   new object[] {1.4f, 1.5f, null, 1.6f, 5.0f},
							   new object[] {20.0f, 2.0f, 1.0f }
							   ),
				/*Neutral*/ new Personality(
							   new System.Type[] { typeof(PacketMoveAwayPlayer), typeof(PacketDistanceAlly), typeof(PacketAttackInRange),
								   typeof(PacketShootInRange), typeof(PacketMoveToPlayer), typeof(PacketRoam)},
							   new object[] {1.4f, 1.5f, null, null, 1, 1.6f, 5.0f},
							   new object[] {15.0f, 2.5f, 1.5f }
							   ),
			};

			System.Type[] types;
			object[] arg;
			public object[] baseSettings;
			//index 0: forget time, index 1:damage reaction cooldown index 2: attack reaction cooldown

			public Personality(System.Type[] types, object[] arg, object[] baseSettings) => (this.types, this.arg, this.baseSettings) = (types, arg, baseSettings);

			public static List<AiPacket> getPacketsFromPersona(Personalities persona, Enemy cur) {
				Personality p = personas[(int)persona];

				List<AiPacket> packs = new List<AiPacket>();

				int argIndex = 0;

				foreach (System.Type t in p.types) {
					if (t == typeof(PacketAttackInRange)) {
						packs.Add(new PacketAttackInRange(cur.slashAttackRange));
						argIndex++;
					} else if (t == typeof(PacketDistanceAlly)) {
						packs.Add(new PacketDistanceAlly((float)p.arg[argIndex++]));
					} else if (t == typeof(PacketMoveAwayPlayer)) {
						packs.Add(new PacketMoveAwayPlayer((float)p.arg[argIndex++]));
					} else if (t == typeof(PacketMoveToPlayer)) {
						packs.Add(new PacketMoveToPlayer((float)p.arg[argIndex++]));
					} else if (t == typeof(PacketRoam)) {
						packs.Add(new PacketRoam((float)p.arg[argIndex++]));
					} else if (t == typeof(PacketShootInRange)) {
						argIndex++;
						packs.Add(new PacketShootInRange(cur.magic1, (int)p.arg[argIndex++]));
					}
				}

				return packs;
			}

			public static object[] getSettingsFromPersona(Personalities persona) {
				return personas[(int)persona].baseSettings;
			}

		}
	}
}