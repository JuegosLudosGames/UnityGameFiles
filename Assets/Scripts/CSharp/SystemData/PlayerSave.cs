using JLG.gift.cSharp.inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.SystemData {
	[System.Serializable]
	public class PlayerSave {
		public SerializableInventory Inventory;
		public float StrengthLevelIncrease;
		public float SpellStrengthLevelIncrease;
		public float MaxHealth;
		public float MaxMana;
		public int buildId;
	}
}