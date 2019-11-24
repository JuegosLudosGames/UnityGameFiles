using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using JLG.gift.cSharp.inventory.scripts;
using UnityEditor;

namespace JLG.gift.cSharp.inventory {
	[CreateAssetMenu(fileName = "new ScriptItem", menuName = "ScriptItemData", order = 53)]
	public class ScriptItem : InvItem {

		[SerializeField]
		[HideInInspector]
		public ScriptItemBase script;
		[HideInInspector]
		public MonoScript s;

		protected override void setTag(StringBuilder sb) {

		}

	}
}