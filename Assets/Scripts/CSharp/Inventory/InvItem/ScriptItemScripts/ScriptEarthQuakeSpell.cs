using JLG.gift.cSharp.entity;
using JLG.gift.cSharp.entity.enemy;
using JLG.gift.cSharp.entity.player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.inventory.scripts {
	public class ScriptEarthQuakeSpell : ScriptItemBase {

		public float radius = 5;
		public float damage = 20;
		public int particles = 4;
		public GameObject particlePreFab;
		public float delay = 5;

		public override void Invoke(Entity e, ItemStack i) {

			//show particles

			//checks if only working with more than 0 particles particles
			if (particles > 0) {
				//checks if only 1 number
				if (particles == 1) {
					//spawn particle at center
					createParticle(e.transform.position);
				} else if (particles == 2) {
					//half raduis
					float hRadius = radius / 2;
					createParticle(new Vector2(e.transform.position.x - hRadius, e.transform.position.y));
					createParticle(new Vector2(e.transform.position.x + hRadius, e.transform.position.y));
				} else {
					float divisions = (radius * 2) / particles;
					float leftMostPoint = e.transform.position.x - radius;
					for (int x = 0; x < particles; x++) {
						createParticle(new Vector2(leftMostPoint + (divisions * x), e.transform.position.y));
					}
				}
			}
			
			//damages

			//getsAllHitboxes of enemies
			Collider2D[] hits = Physics2D.OverlapCapsuleAll(e.transform.position, new Vector2(radius, 1), CapsuleDirection2D.Horizontal, 0, LayerMask.NameToLayer("Attackable"));

			foreach (Collider2D h in hits) {
				if (h.gameObject.tag.Equals("HitBox")) {
					h.gameObject.GetComponentInParent<GameObject>().GetComponentInParent<Enemy>().damage(damage);
					((PlayerController)e).cooldownOnHeldItem(delay, i);
				}
			}

		}

		private void createParticle(Vector2 point) {
			GameObject.Instantiate(particlePreFab, point, new Quaternion(0,0,0,0) ,GameObject.FindGameObjectWithTag("QuickObjectHoler").transform);
		}

	}
}