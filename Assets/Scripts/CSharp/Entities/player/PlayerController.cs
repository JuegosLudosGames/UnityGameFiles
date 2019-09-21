using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JLG.gift.cSharp.attackData.spells.spelltype;
using JLG.gift.cSharp.ui.overlay;
using JLG.gift.cSharp.background.input;
using JLG.gift.cSharp.entity.player.data;
using JLG.gift.cSharp.inventory;
using JLG.gift.cSharp.ui.inventory;
using JLG.gift.cSharp.enviroment.interactble;

namespace JLG.gift.cSharp.entity.player {

	[RequireComponent(typeof(Rigidbody2D))]
	public class PlayerController : Entity {

		[Header("Control")]
		[Header("	Derived from Player")]
		//public float smoothingSpeed = 5.5f;		//speed for smoothing
		public float DashSpeed = 50;
		public float DashDelay = 3.0f;
		public float fallMultiplier = 2.5f;		//speed multiuplier for falling
		public float lowJumpMultiplier = 2f;    //slow multiplier for low jump
		public CircleCollider2D pickupRange;
		public float pickupRangeRadius = 0.75f;
		public Vector2 wallJumpStrength;

		[Header("Basic Attack")]
		//low attack
		public float slashDamage = 0.0f;        //damage for basic attack
		public Collider2D slashRange;           //range for basic attack
		public float slashDelay = 0.0f;

		[Header("First magic")]
		public Spell firstM;
		[Header("Second magic")]
		public Spell secondM;
		//public GameObject spellPre;

		//info bars
		private Slider health_bar;
		private Slider mana_bar;

		private float timeLeftDash = 0.0f;
		//private GradualFader ind1;
		//private GradualFader ind2;
		private inventory.Inventory inventory;
		private PlayerData pd;

		protected override void onAwake() {
			base.onAwake();
			Entity.player = this;
		}

		protected override void onStart() {
			health_bar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();
			mana_bar = GameObject.FindGameObjectWithTag("ManaBar").GetComponent<Slider>();
			//ind1 = GameObject.FindGameObjectWithTag("Spell1Ind").GetComponent<GradualFader>();
			//ind2 = GameObject.FindGameObjectWithTag("Spell2Ind").GetComponent<GradualFader>();
			pickupRange.radius = pickupRangeRadius;
			inventory = PlayerData.instance.inventory;
			pd = PlayerData.instance;

			updateStatData(getTotalAssessoryStats());
		}

		// Update is called once per frame
		protected override void onEarlyUpdate() {

			InventorySystem invSys = GameObject.FindGameObjectWithTag("InventorySystem").GetComponent<InventorySystem>();

			if (timeLeftDash > 0)
				timeLeftDash -= Time.deltaTime;

			//checks to pickup items
			//Pickupable[] pickups 
			List<Pickupable> picks = getItemsInArea();
			if (picks.Capacity > 0) {
				foreach(Pickupable p in picks) {

					int slot = inventory.isSpaceAvaliable(p.item);

					if (slot != -1) {
						ItemStack item = p.pickup();
						//inventory.placeInFirstAvaliable(new ItemStack(item, 1));
						//GameObject.FindGameObjectWithTag("InventorySystem").GetComponent<InventorySystem>().placeItem(slot, item);
						//ItemStack st = new ItemStack(item, 1);
						inventory.mergeSlots(slot, item);
						invSys.merge(slot, item);
					} else {
						break;
					}
				}
			}

			//checks base damage
			float attackDam = slashDamage + pd.strengthInc + pd.buildData.BaseStrength;
			float spellDamAdd = pd.spellInc + pd.buildData.BaseMagicStrength;
			//checks multi damage
			float attackDamMulti = 1;
			float spellDamMulti = 1;

			//finds from assess stats
			float[] assStat = getTotalAssessoryStats();
			attackDam += assStat[5];
			spellDamAdd += assStat[6];
			attackDamMulti *= assStat[0];
			spellDamMulti *= assStat[1];
			float walkSpeedM = assStat[4];
			float attackSpeed = assStat[2];
			float spellSpeed = assStat[3];
			//newH += assStat[10];
			float newHM = assStat[9];
			//newM += assStat[8];
			float newMM = assStat[7];

			updateStatData(assStat);

			
			////sets health and mana
			//maxHealth = newH;
			//maxHealth *= newHM;
			//if (health > maxHealth)
			//	//damage(health - maxHealth);
			//	health = maxHealth;
			//maxMana = newM;
			//maxMana *= newMM;
			//if (mana > maxMana)
			//	//damage(maxMana - mana);
			//	mana = maxMana;

			//Debug.Log("max " + maxHealth + " with " + health + " new" + newH + " and " + newHM);

			InvItem sel = invSys.getSelectedItem();
			if (!(sel is null)) {
				if (sel is WeaponItem) {
					WeaponItem w = sel as WeaponItem;

					attackDam += w.StrengthFixed;
					attackDamMulti *= w.StrengthMulti;

					spellDamAdd += w.SpellFixed;
					spellDamMulti *= w.SpellMulti;

					attackSpeed *= w.AttackSpeedIncreaseMulti;
					spellSpeed *= w.SpellSpeedIncreaseMulti;
					walkSpeedM *= w.WalkSpeedMulti;
				}
			}

			//amount of movement in x direction
			float movementX = 0.0f;
			//amount of force to push character in y direction
			//float forceY = 0.0f;

			//checks if user is pressing keys for movement
			//if (Input.GetKeyDown(KeyInput.Right)) {
			//	//move right
			//	movementX = smoothingSpeed;
			//}
			if (Input.GetKey(KeyInput.Right)) {
				//move right
				if (Input.GetKeyDown(KeyInput.Dash) && timeLeftDash <= 0) {
					movementX = DashSpeed;
					timeLeftDash = DashDelay;
				} else {
					movementX = speed;
				}
			}

			//if (Input.GetKeyDown(KeyInput.Left)) {
			//	//move left
			//	movementX = -smoothingSpeed;
			//}
			if (Input.GetKey(KeyInput.Left)) {
				//move left
				if (Input.GetKeyDown(KeyInput.Dash) && timeLeftDash <= 0) {
					movementX = -DashSpeed;
					timeLeftDash = DashDelay;
				} else {
					movementX = -speed;
				}
			}
			//jump
			if (Input.GetKeyDown(KeyInput.Jump)) {
				if (grounded)
					jump(jumpStrength);
				else if (walled == EntityWallState.LEFT)
					jumpWall(new Vector2(wallJumpStrength.x, wallJumpStrength.y));
				else if (walled == EntityWallState.RIGHT) {
					jumpWall(new Vector2(-wallJumpStrength.x, wallJumpStrength.y));
				} else if (walled == EntityWallState.BOTH) {
					jumpWallBoth(jumpStrength);
				}
					
			}

			//set object to move
			movex(movementX * walkSpeedM);
			//jump(forceY);

			//check if player wants to attack
			//attack main
			if (Input.GetKeyDown(KeyInput.Physical)) {
				if (shouldAttack()) {
					attack(slashRange, (slashDamage + attackDam) * attackDamMulti, (slashDelay / attackSpeed));
					modelAnimator.SetTrigger("Attack");
					interactAll();
				}
			}

			//first attack magic
			//if (Input.GetKeyDown(KeyInput.Magic1)) {
			//	if (shouldShoot(firstM, 1)) {
			//		shoot(firstM, 1, spellDamAdd, spellDamMulti, spellSpeed);
			//		modelAnimator.SetTrigger("Magic");
			//		//Debug.Log("shooting");
			//	}
			//}

			//if (Input.GetKeyDown(KeyInput.Magic2)) {
			//	if (shouldShoot(secondM, 2)) {
			//		shoot(secondM, 2, spellDamAdd, spellDamMulti, spellSpeed);
			//		modelAnimator.SetTrigger("Magic");
			//	}
			//}



			//check for items
			//check if changing slot
			if (Input.GetKeyDown(KeyInput.SwitchItemRight)) {
				//move hotbar right
				//should reset to 0?
				if (invSys.selectedSlot == (invSys.slotLength - 1)) {
					//go to 0
					invSys.selectSlot(0);
				} else {
					//go to next
					invSys.selectSlot(invSys.selectedSlot + 1);
				}
			} else if (Input.GetKeyDown(KeyInput.SwitchItemLeft)) {
				//move hotbar left
				//should reset to 0?
				if (invSys.selectedSlot == 0) {
					//go to 0
					invSys.selectSlot(invSys.slotLength - 1);
				} else {
					//go to next
					invSys.selectSlot(invSys.selectedSlot - 1);
				}
			} else if (Input.GetKeyDown(KeyInput.UseItem)) {
				//check if item present
				int slot = Inventory.HotbarSlotToSlot(invSys.selectedSlot);
				if (inventory.isItemPresent(slot)) {
					ItemStack its = inventory.getItemInSlot(slot);
					if (its.item is ConsumableItem) {
						ConsumableItem i = invSys.consumeSlotOnce() as ConsumableItem;
						pd.consume(i);
						heal(i.HealthRefill);
						refillMana(i.ManaRefill);
					} else if (inventory.getItemInSlot(slot).item is SpellStaffItem) {
						shoot(its, spellDamAdd, spellDamMulti, spellSpeed);
					}
				} 
			}

			
			
		}

		protected override void onLateUpdate() {
			//jump higher on hold
			if (rb.velocity.y < 0) {
				rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
			} else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.W)) {
				rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
			}

			//update bars
			updateInfo();

		}

		protected override void onFixedUpdate() {
			//check walls for Player
			checkWalled();
		}

		protected override void death() {
			base.death();
			updateInfo();
		}

		private void updateInfo() {
			health_bar.value = health / maxHealth;
			mana_bar.value = mana / maxMana;
			//ind1.fill = 1- (timeBeforeNextSpell_1/firstM.Delay);
			//ind2.fill = 1 - (timeBeforeNextSpell_2 / secondM.Delay);
		}

		private void interactAll() {
			foreach (Interactable i in Interactable.currentPlayerRange) {
				i.Interact();
			}
		}

		private List<Pickupable> getItemsInArea() {

			List<Pickupable> re = new List<Pickupable>();

			ContactFilter2D filter = new ContactFilter2D();
			filter.useLayerMask = true;
			filter.SetLayerMask(1 << 13);
			filter.useTriggers = true;

			Collider2D[] results = new Collider2D[20];

			pickupRange.OverlapCollider(filter, results);

			foreach (Collider2D r in results) {
				if (r == null)
					continue;
				re.Add(r.gameObject.GetComponent<Pickupable>());
			}
			return re;
		}

		private float[] getTotalAssessoryStats() {
			float[] ret = new float[11];
			//items are in order as shown below

			ret[0] = 1;
			ret[1] = 1;
			ret[2] = 1;
			ret[3] = 1;
			ret[4] = 1;
			ret[7] = 1;
			ret[9] = 1;

			foreach (AssessoryItem i in inventory.GetAssessoryItems()) {
				ret[0] *= i.StrengthMulti;
				ret[1] *= i.SpellMulti;
				ret[2] *= i.AttackSpeedIncreaseMulti;
				ret[3] *= i.SpellSpeedIncreaseMulti;
				ret[4] *= i.WalkSpeedMulti;
				ret[5] += i.StrengthFixed;
				ret[6] += i.SpellFixed;
				ret[7] *= i.ManaMulti;
				ret[8] += i.ManaFixed;
				ret[9] *= i.HealthMulti;
				ret[10] += i.HealthFixed;
			}
			return ret;
		}

		private void updateStatData(float[] assStat) {
			float newM = pd.buildData.BaseManaLevel;
			float newH = pd.buildData.BaseHealthLevel;

			newM += pd.maxManaAdd;
			newH += pd.maxHealthAdd;

			float newHM = assStat[9];
			float newMM = assStat[7];

			//sets health and mana
			maxHealth = newH;
			maxHealth *= newHM;
			if (health > maxHealth)
				//damage(health - maxHealth);
				health = maxHealth;
			maxMana = newM;
			maxMana *= newMM;
			if (mana > maxMana)
				//damage(maxMana - mana);
				mana = maxMana;
			health = maxHealth;
			mana = maxMana;
		}

	}
}