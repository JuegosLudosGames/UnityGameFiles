using JLG.gift.cSharp.formating;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace JLG.gift.cSharp.inventory {
	[CreateAssetMenu(fileName = "new Weapon", menuName = "WeaponData", order = 53)]
	public class WeaponItem : InvItem {

		protected static readonly string s = "Strength";
		protected static readonly string m = "Spell Damage";
		protected static readonly string sa = "Attack Speed";
		protected static readonly string ws = "Run Speed";
		protected static readonly string ss = "Casting Speed";

		[Header("Weapon Stats")]
		[SerializeField]
		[Range(0, 1)]
		private float strengthMulti;
		[SerializeField]
		[Range(0, 1)]
		private float spellMulti;
		[SerializeField]
		[Range(0, 1)]
		private float attackSpeedIncreaseMulti;
		[SerializeField]
		[Range(0, 1)]
		private float spellSpeedIncreaseMulti;
		[SerializeField]
		[Range(0, 1)]
		private float walkSpeedMulti;
		[SerializeField]
		private float strengthFixed;
		[SerializeField]
		private float spellFixed;

		protected override string addTag {
			get {
				return "$e(Weapon)";
			}
		}

		public float StrengthMulti { get { return strengthMulti + 1; } }
		public float SpellMulti { get { return spellMulti + 1; } }
		public float AttackSpeedIncreaseMulti { get { return attackSpeedIncreaseMulti + 1; } }
		public float SpellSpeedIncreaseMulti { get { return spellSpeedIncreaseMulti + 1; } }
		public float WalkSpeedMulti { get { return walkSpeedMulti + 1; } }
		public float StrengthFixed { get { return strengthFixed; } }
		public float SpellFixed { get { return spellFixed; } }

		protected override void setTag(StringBuilder sb) {

			//sb.AppendLine(TextFormatter.translateCodes("$eWhen held"));
			sb.AppendLine("$eWhen held");

			if (StrengthMulti > 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$a" + s + " +" + InvItem.percent(StrengthMulti) + "%"));
				sb.Append("$a");
				sb.Append(s);
				sb.Append(" +");
				sb.Append(InvItem.percent(StrengthMulti));
				sb.AppendLine("%");
			} else if (StrengthMulti < 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$4" + s + " -" + InvItem.percent(StrengthMulti) + "%"));
				sb.Append("$4");
				sb.Append(s);
				sb.Append(" -");
				sb.Append(InvItem.percent(StrengthMulti));
				sb.AppendLine("%");
			}

			if (strengthFixed > 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$a" + s + " +" + strengthFixed + " damage"));
				sb.Append("$a");
				sb.Append(s);
				sb.Append(" +");
				sb.Append(strengthFixed);
				sb.AppendLine(" damage");
			} else if (strengthFixed < 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$4" + s + " -" + strengthFixed + " damage"));
				sb.Append("$4");
				sb.Append(s);
				sb.Append(" -");
				sb.Append(strengthFixed);
				sb.AppendLine(" damage");
			}

			if (spellMulti > 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$a" + m + " +" + InvItem.percent(spellMulti) + "%"));
				sb.Append("$a");
				sb.Append(m);
				sb.Append(" +");
				sb.Append(InvItem.percent(spellMulti));
				sb.AppendLine("%");
			} else if (spellMulti < 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$4" + m + " -" + InvItem.percent(spellMulti) + "%"));
				sb.Append("$4");
				sb.Append(m);
				sb.Append(" -");
				sb.Append(InvItem.percent(spellMulti));
				sb.AppendLine("%");
			}

			if (spellFixed > 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$a" + m + " +" + spellFixed + " damage"));
				sb.Append("$a");
				sb.Append(m);
				sb.Append(" +");
				sb.Append(spellFixed);
				sb.AppendLine(" damage");
			} else if (spellFixed < 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$4" + m + " -" + spellFixed + " damage"));
				sb.Append("$4");
				sb.Append(m);
				sb.Append(" -");
				sb.Append(spellFixed);
				sb.AppendLine(" damage");
			}

			if (attackSpeedIncreaseMulti > 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$a" + sa + " +" + InvItem.percent(attackSpeedIncreaseMulti) + " %"));
				sb.Append("$a");
				sb.Append(sa);
				sb.Append(" +");
				sb.Append(InvItem.percent(attackSpeedIncreaseMulti));
				sb.AppendLine("%");
			} else if (attackSpeedIncreaseMulti < 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$4" + sa + " -" + InvItem.percent(attackSpeedIncreaseMulti) + " %"));
				sb.Append("$4");
				sb.Append(sa);
				sb.Append(" -");
				sb.Append(InvItem.percent(attackSpeedIncreaseMulti));
				sb.AppendLine("%");
			}

			if (spellSpeedIncreaseMulti > 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$a" + ss + " +" + InvItem.percent(spellSpeedIncreaseMulti) + " %"));
				sb.Append("$a");
				sb.Append(ss);
				sb.Append(" +");
				sb.Append(InvItem.percent(spellSpeedIncreaseMulti));
				sb.AppendLine("%");
			} else if (spellSpeedIncreaseMulti < 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$4" + ss + " -" + InvItem.percent(spellSpeedIncreaseMulti) + " %"));
				sb.Append("$4");
				sb.Append(ss);
				sb.Append(" -");
				sb.Append(InvItem.percent(spellSpeedIncreaseMulti));
				sb.AppendLine("%");
			}

			if (walkSpeedMulti > 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$a" + ws + " +" + InvItem.percent(walkSpeedMulti) + " %"));
				sb.Append("$a");
				sb.Append(ws);
				sb.Append(" +");
				sb.Append(InvItem.percent(walkSpeedMulti));
				sb.AppendLine("%");
			} else if (walkSpeedMulti < 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$4" + ws + " -" + InvItem.percent(walkSpeedMulti) + " %"));
				sb.Append("$4");
				sb.Append(ws);
				sb.Append(" -");
				sb.Append(InvItem.percent(walkSpeedMulti));
				sb.AppendLine("%");
			}

		}

	}
}