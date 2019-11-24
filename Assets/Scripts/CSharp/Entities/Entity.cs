using JLG.gift.cSharp.attackData.spells.controller;
using JLG.gift.cSharp.attackData.spells.spelltype;
using JLG.gift.cSharp.entity.player;
using JLG.gift.cSharp.background.sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JLG.gift.cSharp.background.scene;
using JLG.gift.cSharp.inventory;

namespace JLG.gift.cSharp.entity {

	[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpellController))]
	[RequireComponent(typeof(AudioSource))]
	public abstract class Entity : MonoBehaviour {

		//static code
		//static player Entity
		public static Entity player;
		public static HashSet<Entity> objects = new HashSet<Entity>(); //uni's part of the code

		//spawn an entity
		public static Entity spawn(Entity en, Vector3 pos) {
			return spawn(en, pos, null);
		}

		//spawns an entity inside a certain gameobject
		public static Entity spawn(Entity en, Vector3 pos, Transform parent) {
			return (Entity)Instantiate(en, pos, new Quaternion(0, 0, 0, 0), parent);
		}

		//despawns all entities except the player
		public static void despawnAllButPlayer() {
			foreach (Entity en in objects) {
				en.despawn();
			}
		}


		//components
		protected Rigidbody2D rb;
		protected Animator modelAnimator;
		protected SpellController spellCon;
		protected AudioSource AudioS;

		//public
		[Header("Logic and Physics")]
		[Header("	Derived from Entity")]
		public bool isEnabled = true;       //should let the object work
		public float SmoothingX = 0.15f;    //smoothing amount for X axis
		public float verticalDrag = 0.95f;  //Vertical drag option of entity
		public float horizontalDrag = 0.5f; //horiznal drag option of entity
		public LayerMask whatIsGround;
		public float groundedRadius = 0.2f;
		public Transform[] groundChecks;
		public EntityWallState walled = EntityWallState.NONE;
		public LayerMask whatIsWall;
		public float walledRadius = 0.2f;
		public Transform[] wallCheckR;
		public Transform[] wallCheckL;

		[Header("Control")]
		public float maxHeight = 5.0f;      //max height of entity
		public float speed = 12.5f;         //max speed of entity
		public float jumpStrength = 10.5f;  //strength of jump of entity
		public float despawnTime = 5.0f;
		public float maxHealth = 20.0f;
		public float health {
			get { return healthI; } protected set { healthI = value; }
		}
		public Collider2D hitBox;           //the active hitbox of the entity
		public float maxMana = 0;
		public float mana {
			get; protected set;
		}

		//function variables
		[HideInInspector]
		public bool grounded = false;      //is it on the ground?
		[HideInInspector]
		public bool isDespawning = false;  //are we currently trying to despawn it?
		[HideInInspector]
		public float timeToDespawn = 0.0f; //time left before despawn
		private float healthI = 0.0f;       //0 is none, 1 is full health of entity
		[HideInInspector]
		public float timeBeforeNextAttack = 0.0f;  //delay before being able to attack again
		public float timeBeforeNextSpell_1 {
			get; private set;
		}
		public float timeBeforeNextSpell_2 {
			get; private set;
		}
		public bool continueLookingdir {
			get; set;
		}

		private Vector2 m_Velocity = Vector2.zero; //reference for movement

		// Start is called before the first frame update
		void Start() {
			//gets pointers for gameObjects
			rb = GetComponent<Rigidbody2D>();
			modelAnimator = GetComponentInChildren<Animator>();
			spellCon = GetComponent<SpellController>();
			AudioS = GetComponent<AudioSource>();
			AudioS.loop = false;
			healthI = maxHealth;
			mana = maxMana;

			//adds entity to list
			objects.Add(this);

			//calls start to entity's class
			onStart();
		}

		private void Awake() {
			//calls awake to entity's class
			onAwake();
		}

		protected virtual void onAwake() { }

		//called when gameobject is being started
		protected virtual void onStart() { }
		//called before any Regular Update Actions
		protected virtual void onEarlyUpdate() { }
		//called aftter any Regular Update Actions
		protected virtual void onLateUpdate() { }

		// Update is called once per frame
		void Update() {
			if (!SceneController.currentScene.enableEntites)
				return;

			//is entity being despawned
			if (isDespawning) {
				//subtract from time
				timeToDespawn -= Time.deltaTime;
				//is time up?
				if (timeToDespawn <= 0) {
					//depsawn
					despawn();
				}
			}

			//is the entity enabled
			if (!isEnabled) {
				//stop its movement
				rb.velocity = Vector3.zero;
				//stop method
				return;
			}

			//does sound updates
			if (SoundOptions.instance != null) {
				AudioS.volume = SoundOptions.instance.SfxVolume / 100;
				AudioS.mute = SoundOptions.instance.sfxMute;
			}

			//does early update actions of entity
			onEarlyUpdate();

			//is the entity delaying attack
			if (!shouldAttack()) {
				//subtract time
				timeBeforeNextAttack -= Time.deltaTime;
			}

			//is entity delaying spell 1
			if (!shouldShoot(1)) {
				//subtract time
				timeBeforeNextSpell_1 -= Time.deltaTime;
			}

			//is entity delaying spell 2
			if (!shouldShoot(2)) {
				//subtract time
				timeBeforeNextSpell_2 -= Time.deltaTime;
			}

			//does update for all entities
			//move the entity
			Vector2 newVel = new Vector2();
			Vector2 newVelx = new Vector2();
			Vector2 newVely = new Vector2();
			newVelx.x = (movementX == 0) ? rb.velocity.x : movementX;
			newVely.y = (forceY == 0) ? rb.velocity.y : forceY;
			forceY = 0;

			//sets new velocity
			//rb.velocity = newVel;
			newVelx = Vector2.SmoothDamp(rb.velocity, newVelx, ref m_Velocity, SmoothingX);
			//newVely = Vector2.SmoothDamp(rb.velocity, newVely, ref m_Velocity, SmoothingY);

			newVel.x = newVelx.x;
			newVel.y = newVely.y;
			rb.velocity = newVel;

			//newVel.y = vely;
			//rb.velocity = newVel;

			//checks and flips entity to facing direction
			if (!continueLookingdir) {
				if (newVel.x < -0.5) {
					transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
				} else if (newVel.x > 0.5) {
					transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
				}
			}

			movementX = 0;

			//animates entity
			if (Mathf.Abs(newVel.x) >= 0.5f) {
				modelAnimator.SetBool("isRunning", true);
			} else {
				modelAnimator.SetBool("isRunning", false);
			}

			modelAnimator.SetBool("isGrounded", grounded);

			//does late update actions of entity
			onLateUpdate();
		}

		//called after any Fixed Update Actions
		protected virtual void onFixedUpdate() { }

		private void FixedUpdate() {
			//if entity is not enabled, skips
			if (!isEnabled)
				return;

			//does the ground check for entity
			checkGrounded();

			//sets drag to entity
			Vector2 newVel = new Vector2(rb.velocity.x, rb.velocity.y);

			//each component has different drag
			newVel.x *= horizontalDrag;
			newVel.y *= verticalDrag;

			//applies drag
			rb.velocity = newVel;

			//does fixed update actions of entity
			onFixedUpdate();
		}

		//heals the entity
		public void heal(float hp) {
			//adds health points
			health += hp;
			//checks if health is too high
			if (health > maxHealth)
				health = maxHealth; //fixes
		}

		//heals the entity to full
		public void heal() {
			health = maxHealth;
		}

		//refills mana
		public void refillMana(float mp) {
			//adds mp
			mana += mp;
			//if too high, fix
			if (mana > maxMana)
				mana = maxMana;
		}

		//refills mana to full
		public void refillMana() {
			mana = maxMana;
		}

		//has entity attacks with damage and range
		protected void attack(Collider2D range, float damage, float time) {
			//gets all attackable entites
			List<Entity> entities = getAttackableEntities(range);
			//goes through each
			foreach (Entity en in entities) {
				//damages it
				en.damage(damage);
			}
			timeBeforeNextAttack = time;
			//random attack sound
			AudioS.clip = SoundOptions.instance.attack;
			AudioS.Play();
		}

		//has entity attacks player with damage and range
		protected virtual void attackPlayer(Collider2D range, float damage, float time) {
			//checks if player is in range
			if (isPlayerInRange(range)) {
				//damages player
				player.damage(damage);
			}
			//sets delay
			timeBeforeNextAttack = time;
			//random attack sound
			AudioS.clip = SoundOptions.instance.attack;
			AudioS.Play();
		}

		//shoots a spell
		protected void shoot(Spell spell, int a, float damAdd, float damMulti, float multiDelay = 1) {
			//check if able to shoot
			if (shouldShoot(spell, a)) {
				
				//uses spell controller to fire
				spellCon.fire(spell, this, this is PlayerController, damAdd, damMulti);

				//takes away mana
				this.mana -= spell.Mana;
				//animates
				modelAnimator.SetTrigger("Magic");

				//sets delay to appropiate watch
				if (a == 1) {
					timeBeforeNextSpell_1 = spell.Delay / multiDelay;
				} else {
					timeBeforeNextSpell_2 = spell.Delay / multiDelay;
				}
				
			}
		}

		protected void shoot(ItemStack spell, float damAdd, float damMulti, float multiDelay = 1) {
			//if(shouldShoot(spell))
			if (shouldShoot(spell)) {

				//sets aside spell
				Spell sp = (spell.item as SpellStaffItem).Spell;

				//fires spell
				spellCon.fire(sp, this, true, damAdd, damMulti);

				//takes away mana
				this.mana -= sp.Mana;

				//animates
				modelAnimator.SetTrigger("Magic");

				//sets delay
				spell.cooldownSet(sp.Delay / multiDelay, sp.Delay);
			}
		}

		//checks if able to shoot indicated spell based on time
		public bool shouldShoot(int a) {
			return (a == 1? timeBeforeNextSpell_1 : timeBeforeNextSpell_2) <= 0;
		}

		//checks if able to shoot indicated spell based on time and mana
		public bool shouldShoot(Spell spell, int a) {
			return shouldShoot(a) && (this.mana >= spell.Mana);
		}

		public bool shouldShoot(ItemStack spell) {
			return !spell.isCooling;
		}

		//checks if able to attack
		public bool shouldAttack() {
			return timeBeforeNextAttack <= 0;
		}


		//protected void increaseResistance(float percent) {
		//	resistance *= percent;
		//}

		//damages the entity
		public virtual void damage(float damage) {
			//remove from health
			healthI -= damage;

			modelAnimator.SetTrigger("Damaged");
			//should it be dead?
			if (health < 0) {
				//kill it
				death();
				return;
			}
		}

		//kills entity
		protected virtual void death() {
			//disable object
			isEnabled = false;
			//set death animation
			modelAnimator.SetTrigger("Die");
			//modelAnimator.enabled = false;
			if (!(this is PlayerController)) {
				despawnWait(despawnTime);
			}
		}

		//sets a timer to despawn
		protected void despawnWait(float time) {
			timeToDespawn = time;
			isDespawning = true;
			hitBox.isTrigger = true;
		}

		//despawns entity
		public void despawn() {
			GameObject.Destroy(this.gameObject);
			objects.Remove(this);
		}

		public static int maxColliderTest = 20;

		//returns all attackable enemies to be damaged
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
			foreach(Collider2D test in results) {
				//if reached the end of teh list or error
				if (test == null)
					continue;
				//is this a hitbox?
				//if (test == test.gameObject.GetComponentInParent<Transform>().GetComponentInParent<Entity>().hitBox) {
				if (test.gameObject.GetComponentInParent<Transform>().GetComponentInParent<Entity>() != null) {
					if (test == test.gameObject.GetComponentInParent<Transform>().GetComponentInParent<Entity>().hitBox && test.gameObject.GetComponentInParent<Transform>().GetComponentInParent<Entity>() != this) {
						//add to list
						entites.Add(test.gameObject.GetComponentInParent<Transform>().GetComponentInParent<Entity>());
					}
				}
			}

			//return list
			return entites;
		}

		//checks if player can be attacked
		public bool isPlayerInRange(Collider2D range) {
			//Debug.Log("checking");
			//creates filter
			ContactFilter2D filter = new ContactFilter2D();
			filter.useLayerMask = true;
			filter.SetLayerMask(1 << 12); //filter only attackable enemies
			//filter.layerMask = 1 << 12;
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

		public float movementX = 0.0f;
		public float forceY = 0.0f;

		//moves entity in the x direction with set velocity
		protected void movex(float movement) {
			movementX += movement;
		}

		//allows entity to jump with force
		protected void jump(float force) {
			if(grounded && force != 0) {
				forceY = force;
				grounded = false;
			}
			modelAnimator.SetTrigger("Jump");
		}

		protected void jumpWall(Vector2 force) {
			forceY = force.y;
			movementX += force.x;
			walled = EntityWallState.NONE;
		}

		protected void jumpWallBoth(float force) {
			forceY = force;
			walled = EntityWallState.NONE;
		}

		//checks if entity is looking in the direction of the position
		public bool isLookingInDirection(Vector3 pos) {
			//checks if looking right
			if (isLookingInDirection()) {
				//is the position to the right
				if ((pos.x - transform.position.x) >= 0) {
					//yes
					return true;
				}

			} else {
				//pos is on the left
				//is item to the left
				if ((pos.x - transform.position.x) <= 0) {
					//yes
					return true;
				}
			}
			//no
			return false;
		}

		//checks if entity is looking in the indicated direction
		public bool isLookingInDirection(bool isRight) {
			//is looking right
			if (isLookingInDirection()) {
				//return appropiate
				return (isRight ? true : false);
				//looking left
			} else {
				//appropiate
				return (isRight ? false : true);
			}
		}

		//checks if looking right
		public bool isLookingInDirection() {
			return transform.localScale.x > 0;
		}

		//checks if the entity is on the ground
		private void checkGrounded() {

			//creates list for grounds
			List<Collider2D> grounds = new List<Collider2D>();

			//gets the grounds on all checks
			foreach (Transform t in groundChecks) {
				Collider2D[] g = Physics2D.OverlapCircleAll(t.position, groundedRadius, whatIsGround);
				//adds to end of list
				grounds.AddRange(g);
			}

			//checks if valid and if so grounds
			foreach (Collider2D c in grounds) {
				if (c.gameObject != gameObject) {
					grounded = true;
					return;
				}
			}
			grounded = false;

		}

		protected void checkWalled() {

			//creates list for walls
			List<Collider2D> wallsR = new List<Collider2D>();

			//gets the walls on all checks for the Right
			foreach (Transform t in wallCheckR) {
				Collider2D[] w = Physics2D.OverlapCircleAll(t.position, walledRadius, whatIsWall);
				//adds to end of list
				wallsR.AddRange(w);
			}

			bool wallR = false;

			//checks if valid and if so grounds
			foreach (Collider2D c in wallsR) {
				if (c.gameObject != gameObject) {
					wallR = true;
				}
			}

			//creates list for walls for Left
			List<Collider2D> wallsL = new List<Collider2D>();

			//gets the walls on all checks for the Right
			foreach (Transform t in wallCheckL) {
				Collider2D[] w = Physics2D.OverlapCircleAll(t.position, walledRadius, whatIsWall);
				//adds to end of list
				wallsL.AddRange(w);
			}

			bool wallL = false;

			//checks if valid and if so grounds
			foreach (Collider2D c in wallsL) {
				if (c.gameObject != gameObject) {
					wallL = true;
				}
			}

			if (wallL && wallR)
				walled = EntityWallState.BOTH;
			else if (wallL)
				walled = (isLookingInDirection(true))? EntityWallState.LEFT : EntityWallState.RIGHT;
			else if (wallR)
				walled = (isLookingInDirection(true)) ? EntityWallState.RIGHT : EntityWallState.LEFT;
			else
				walled = EntityWallState.NONE;
		}

		public enum EntityWallState {
			NONE, LEFT, RIGHT, BOTH
		}


	}
}