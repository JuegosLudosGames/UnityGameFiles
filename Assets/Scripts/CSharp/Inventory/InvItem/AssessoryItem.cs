using JLG.gift.cSharp.formating;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace JLG.gift.cSharp.inventory {
	[CreateAssetMenu(fileName = "new Assessory", menuName = "AssessoryData", order = 53)]
	public class AssessoryItem : InvItem {

		private static readonly string basetag = "When in an AssessorySlot:";

		private static readonly string strengthMulti_displayName = "Strength";
		private static readonly string spellMulti_displayName = "Spell Damage";
		private static readonly string attackSpeedMulti_displayName = "Attack Speed";
		private static readonly string spellSpeedMulti_displayName = "Casting Speed";
		private static readonly string walkSpeedMulti_displayName = "Run Speed";
		//private static readonly string attackSpeedMulti_displayName = "Attack Speed";
		private static readonly string manaMulti_displayName = "Max Mana";
		private static readonly string healthMulti_displayName = "Max Health";

		[Header("Assessory Buffs")]
		[SerializeField]
		[Range(0,1)]
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
		[SerializeField]
		[Range(0, 1)]
		private float manaMulti;
		[SerializeField]
		private float manaFixed;
		[SerializeField]
		[Range(0, 1)]
		private float healthMulti;
		[SerializeField]
		private float healthFixed;

		protected override string addTag {
			get {
				return "$e(Accessory)";
			}
		}

		public float StrengthMulti { get { return strengthMulti + 1; } }
		public float SpellMulti { get { return spellMulti + 1; } }
		public float AttackSpeedIncreaseMulti { get { return attackSpeedIncreaseMulti + 1; } }
		public float SpellSpeedIncreaseMulti { get { return spellSpeedIncreaseMulti + 1; } }
		public float WalkSpeedMulti { get { return walkSpeedMulti + 1; } }
		public float StrengthFixed { get { return strengthFixed; } }
		public float SpellFixed { get { return spellFixed; } }
		public float ManaMulti { get { return manaMulti + 1; } }
		public float ManaFixed { get { return manaFixed; } }
		public float HealthMulti { get { return healthMulti + 1; } }
		public float HealthFixed { get { return healthFixed; } }

		protected override void setTag(StringBuilder sb) {

			//sb.AppendLine(TextFormatter.translateCodes("$eWhen in Assessory Slot"));
			sb.AppendLine("$eWhen in Assessory Slot");

			if (strengthMulti > 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$a" + strengthMulti_displayName + " +" + InvItem.percent(StrengthMulti) + "%"));
				sb.Append("$a");
				sb.Append(strengthMulti_displayName);
				sb.Append(" +");
				sb.Append(InvItem.percent(StrengthMulti));
				sb.AppendLine("%");
			} else if (strengthMulti < 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$4" + strengthMulti_displayName + " -" + InvItem.percent(StrengthMulti) + "%"));
				sb.Append("$4");
				sb.Append(strengthMulti_displayName);
				sb.Append(" -");
				sb.Append(InvItem.percent(StrengthMulti));
				sb.AppendLine("%");
			}

			if (strengthFixed > 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$a" + strengthMulti_displayName + " +" + strengthFixed + " damage"));
				sb.Append("$a");
				sb.Append(strengthMulti_displayName);
				sb.Append(" +");
				sb.Append(strengthFixed);
				sb.AppendLine(" damage");
			} else if (strengthFixed < 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$4" + strengthMulti_displayName + " -" + strengthFixed + " damage"));
				sb.Append("$4");
				sb.Append(strengthMulti_displayName);
				sb.Append(" -");
				sb.Append(strengthFixed);
				sb.AppendLine(" damage");
			}

			if (spellMulti > 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$a" + spellMulti_displayName + " +" + InvItem.percent(spellMulti) + "%"));
				sb.Append("$a");
				sb.Append(spellMulti_displayName);
				sb.Append(" +");
				sb.Append(InvItem.percent(spellMulti));
				sb.AppendLine("%");
			} else if (spellMulti < 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$4" + spellMulti_displayName + " -" + InvItem.percent(spellMulti) + "%"));
				sb.Append("$4");
				sb.Append(spellMulti_displayName);
				sb.Append(" -");
				sb.Append(InvItem.percent(spellMulti));
				sb.AppendLine("%");
			}

			if (spellFixed > 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$a" + spellMulti_displayName + " +" + spellFixed + " damage"));
				sb.Append("$a");
				sb.Append(spellMulti_displayName);
				sb.Append(" +");
				sb.Append(spellFixed);
				sb.AppendLine(" damage");
			} else if (spellFixed < 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$4" + spellMulti_displayName + " -" + spellFixed + " damage"));
				sb.Append("$4");
				sb.Append(spellMulti_displayName);
				sb.Append(" -");
				sb.Append(spellFixed);
				sb.AppendLine(" damage");
			}

			if (attackSpeedIncreaseMulti > 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$a" + attackSpeedMulti_displayName + " +" + InvItem.percent(attackSpeedIncreaseMulti) + "%"));
				sb.Append("$a");
				sb.Append(attackSpeedMulti_displayName);
				sb.Append(" +");
				sb.Append(InvItem.percent(attackSpeedIncreaseMulti));
				sb.AppendLine("%");
			} else if (attackSpeedIncreaseMulti < 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$4" + attackSpeedMulti_displayName + " -" + InvItem.percent(attackSpeedIncreaseMulti) + "%"));
				sb.Append("$4");
				sb.Append(attackSpeedMulti_displayName);
				sb.Append(" -");
				sb.Append(InvItem.percent(attackSpeedIncreaseMulti));
				sb.AppendLine("%");
			}

			if (spellSpeedIncreaseMulti > 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$a" + spellSpeedMulti_displayName + " +" + InvItem.percent(spellSpeedIncreaseMulti) + "%"));
				sb.Append("$a");
				sb.Append(spellSpeedMulti_displayName);
				sb.Append(" +");
				sb.Append(InvItem.percent(spellSpeedIncreaseMulti));
				sb.AppendLine("%");
			} else if (spellSpeedIncreaseMulti < 0) {
				sb.AppendLine(TextFormatter.translateCodes("$4" + spellSpeedMulti_displayName + " -" + InvItem.percent(spellSpeedIncreaseMulti) + "%"));
				sb.Append("$4");
				sb.Append(spellSpeedMulti_displayName);
				sb.Append(" -");
				sb.Append(InvItem.percent(spellSpeedIncreaseMulti));
				sb.AppendLine("%");
			}

			if (manaMulti > 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$a" + manaMulti_displayName + " +" + InvItem.percent(manaMulti) + "%"));
				sb.Append("$a");
				sb.Append(manaMulti_displayName);
				sb.Append(" +");
				sb.Append(InvItem.percent(manaMulti));
				sb.AppendLine("%");
			} else if (manaMulti < 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$4" + manaMulti_displayName + " -" + InvItem.percent(manaMulti) + "%"));
				sb.Append("$4");
				sb.Append(manaMulti_displayName);
				sb.Append(" -");
				sb.Append(InvItem.percent(manaMulti));
				sb.AppendLine("%");
			}

			if (manaFixed > 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$a" + manaMulti_displayName + " +" + manaFixed + " mp"));
				sb.Append("$a");
				sb.Append(manaMulti_displayName);
				sb.Append(" +");
				sb.Append(manaFixed);
				sb.AppendLine(" mp");
			} else if (manaFixed < 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$4" + manaMulti_displayName + " -" + manaFixed + " mp"));
				sb.Append("$4");
				sb.Append(manaMulti_displayName);
				sb.Append(" -");
				sb.Append(manaFixed);
				sb.AppendLine(" mp");
			}

			if (healthMulti > 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$a" + healthMulti_displayName + " +" + InvItem.percent(healthMulti) + "%"));
				sb.Append("$a");
				sb.Append(healthMulti_displayName);
				sb.Append(" +");
				sb.Append(InvItem.percent(healthMulti));
				sb.AppendLine("%");
			} else if (healthMulti < 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$4" + healthMulti_displayName + " -" + InvItem.percent(healthMulti) + "%"));
				sb.Append("$4");
				sb.Append(healthMulti_displayName);
				sb.Append(" -");
				sb.Append(InvItem.percent(healthMulti));
				sb.AppendLine("%");
			}

			if (healthFixed > 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$a" + healthMulti_displayName + " +" + healthFixed + " hp"));
				sb.Append("$a");
				sb.Append(healthMulti_displayName);
				sb.Append(" +");
				sb.Append(healthFixed);
				sb.AppendLine(" hp");
			} else if (healthFixed < 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$4" + healthMulti_displayName + " -" + healthFixed + " hp"));
				sb.Append("$4");
				sb.Append(healthMulti_displayName);
				sb.Append(" -");
				sb.Append(healthFixed);
				sb.AppendLine(" hp");
			}

			if (walkSpeedMulti > 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$a" + walkSpeedMulti_displayName + " +" + InvItem.percent(walkSpeedMulti) + "%"));
				sb.Append("$a");
				sb.Append(walkSpeedMulti_displayName);
				sb.Append(" +");
				sb.Append(InvItem.percent(walkSpeedMulti));
				sb.AppendLine("%");
			} else if (walkSpeedMulti < 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$4" + walkSpeedMulti_displayName + " -" + InvItem.percent(walkSpeedMulti) + "%"));
				sb.Append("$4");
				sb.Append(walkSpeedMulti_displayName);
				sb.Append(" -");
				sb.Append(InvItem.percent(walkSpeedMulti));
				sb.AppendLine("%");
			}

		}

	}
}