using JLG.gift.cSharp.attackData.spells.spelltype;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace JLG.gift.cSharp.inventory {
	[CreateAssetMenu(fileName = "new StaffItem", menuName = "SpellStaffItemData", order = 53)]
	public class SpellStaffItem : InvItem {
		[Header("SpellStaff")]
		[SerializeField]
		private Spell spell;
		
		public Spell Spell { get { return spell; } }

		protected override string addTag {
			get {
				return "$e(Spell Staff Item)";
			}
		}

		protected override void setTag(StringBuilder sb) {

			if (!(spell is null)) {

				sb.Append("$9");
				sb.AppendLine(spell.SpellName);

				sb.Append("$a");
				sb.Append(spell.Damage.ToString());
				sb.AppendLine(" damage");

				sb.Append("$b");
				sb.Append(spell.Mana.ToString());
				sb.AppendLine(" mana");
			}
		}

	}
}