using JLG.gift.cSharp.entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.inventory.scripts {
	[System.Serializable]
	public class ScriptItemBase : ScriptableObject {

		public virtual void Invoke(Entity e, ItemStack i) {}
		public virtual string Tag() {
			return "General Item";
		}

	}
}