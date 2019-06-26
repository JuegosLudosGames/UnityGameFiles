using JLG.gift.cSharp.attackData.spells.spellObject;
using JLG.gift.cSharp.attackData.spells.spelltype;
using JLG.gift.cSharp.entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.attackData.spells.controller {
	public class SpellController : MonoBehaviour {

		//public GameObject projectileParent;

		//fires a projectile
		public void fire(Spell spell, Entity en, bool player, float damAdd, float damMulti) {
			//clones prefab into scene
			GameObject projectileParent = GameObject.FindGameObjectWithTag("QuickObjectHoler");
			GameObject got = GameObject.Instantiate(spell.PreObject, transform.position, new Quaternion(0, 0, 0, 0), projectileParent.transform);
			got.GetComponent<SpellProjectile>().type = spell;
			
			//sets visual sprites
			SpriteRenderer s = got.GetComponent<SpriteRenderer>();
			s.sprite = spell.Icon;
			s.color = spell.IconC;

			//setups projectile for attacking
			got.GetComponent<SpellProjectile>().setInfo(player, gameObject.transform.localScale.x > 0, en, damAdd, damMulti);
			got.GetComponent<SpellProjectile>().startP();
		}

	}
}