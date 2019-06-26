using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.attackData.spells.spelltype {
	[CreateAssetMenu(fileName = "new SpellData", menuName = "SpellData", order = 51)]
	[System.Serializable]
	public class Spell : ScriptableObject {

		[SerializeField]
		private string spellName;
		[SerializeField]
		private float range;
		[SerializeField]
		private float lifeTime;
		[SerializeField]
		private float damage;
		[SerializeField]
		private float speed;
		[SerializeField]
		private float mana;
		[SerializeField]
		private bool singleOnly;
		//[SerializeField]
		//private GameObject objectTexture;
		[SerializeField]
		private GameObject preObject;
		[SerializeField]
		private float delay;
		[SerializeField]
		private Sprite icon;
		[SerializeField]
		private Color iconc;


		public string SpellName { get { return spellName; } }		
		public float Range { get { return range; } }		
		public float LifeTime { get { return lifeTime; } }		
		public float Damage { get { return damage; } }		
		public float Speed { get { return speed; } }		
		public float Mana { get { return mana; } }		
		public bool SingleOnly { get { return singleOnly; } }
		public float Delay { get { return delay; } }
		//public GameObject ObjectTexture { get { return objectTexture; } }
		public GameObject PreObject { get { return preObject; } }
		public Sprite Icon { get { return icon; } }
		public Color IconC { get { return iconc; } }

	}
}