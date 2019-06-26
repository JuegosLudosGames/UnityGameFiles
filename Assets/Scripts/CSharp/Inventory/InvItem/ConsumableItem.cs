using JLG.gift.cSharp.formating;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace JLG.gift.cSharp.inventory {
	[CreateAssetMenu(fileName = "new Consumable", menuName = "ConsumableItemData", order = 53)]
	public class ConsumableItem : InvItem {

		protected static readonly string hpR = "Health";
		protected static readonly string mpR = "Mana";

		protected static readonly string hpP = "Max Health";
		protected static readonly string mpP = "Max Mana";
		protected static readonly string sp = "Max Strength";
		protected static readonly string spp = "Max Spell Damage";

		[Header("Support")]
		[SerializeField]
		private float healthRefill;
		[SerializeField]
		private float manaRefill;

		[Header("Stat ups")]
		[SerializeField]
		private float permHealthIncrease;
		[SerializeField]
		private float permManaIncrease;
		[SerializeField]
		private float permStrengthIncrease;
		[SerializeField]
		private float permSpellIncrease;

		protected override string addTag {
			get {
				return "$e(Consumable)";
			}
		}

		public float HealthRefill { get => healthRefill; }
		public float ManaRefill { get => manaRefill; }
		public float PermHealthIncrease { get => permHealthIncrease; }
		public float PermManaIncrease { get => permManaIncrease; }
		public float PermStrengthIncrease { get => permStrengthIncrease; }
		public float PermSpellIncrease { get => permSpellIncrease; }

		protected override void setTag(StringBuilder sb) {

			//sb.AppendLine(TextFormatter.translateCodes("$eWhen consumed"));
			sb.AppendLine("$eWhen consumed");

			if (healthRefill > 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$a" + hpR + " +" + healthRefill + " hp"));
				sb.Append("$a");
				sb.Append(hpR);
				sb.Append(" +");
				sb.Append(healthRefill);
				sb.AppendLine(" hp");
			} else if (healthRefill < 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$4" + hpR + " -" + healthRefill + " hp"));
				sb.Append("$4");
				sb.Append(hpR);
				sb.Append(" -");
				sb.Append(healthRefill);
				sb.AppendLine(" hp");
			}

			if (manaRefill > 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$a" + mpR + " +" + manaRefill + " mp"));
				sb.Append("$a");
				sb.Append(mpR);
				sb.Append(" +");
				sb.Append(manaRefill);
				sb.AppendLine(" mp");
			} else if (manaRefill < 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$4" + mpR + " -" + manaRefill + " mp"));
				sb.Append("$4");
				sb.Append(mpR);
				sb.Append(" -");
				sb.Append(manaRefill);
				sb.AppendLine(" mp");
			}

			if (permHealthIncrease > 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$a" + hpP + " +" + permHealthIncrease + " hp"));
				sb.Append("$a");
				sb.Append(hpP);
				sb.Append(" +");
				sb.Append(permHealthIncrease);
				sb.AppendLine(" hp");
			} else if (permHealthIncrease < 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$4" + hpP + " -" + permHealthIncrease + " hp"));
				sb.Append("$4");
				sb.Append(hpP);
				sb.Append(" -");
				sb.Append(permHealthIncrease);
				sb.AppendLine(" hp");
			}

			if (permManaIncrease > 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$a" + mpP + " +" + permManaIncrease + " mp"));
				sb.Append("$a");
				sb.Append(mpP);
				sb.Append(" +");
				sb.Append(permManaIncrease);
				sb.AppendLine(" mp");
			} else if (permManaIncrease < 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$4" + mpP + " -" + permManaIncrease + " mp"));
				sb.Append("$4");
				sb.Append(mpP);
				sb.Append(" -");
				sb.Append(permManaIncrease);
				sb.AppendLine(" mp");
			}

			if (permStrengthIncrease > 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$a" + sp + " +" + permStrengthIncrease + " damage"));
				sb.Append("$a");
				sb.Append(sp);
				sb.Append(" +");
				sb.Append(permStrengthIncrease);
				sb.AppendLine(" damage");
			} else if (permStrengthIncrease < 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$4" + sp + " -" + permStrengthIncrease + " damage"));
				sb.Append("$4");
				sb.Append(sp);
				sb.Append(" -");
				sb.Append(permStrengthIncrease);
				sb.AppendLine(" damage");
			}

			if (PermSpellIncrease > 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$a" + spp + " +" + PermSpellIncrease + " damage"));
				sb.Append("$a");
				sb.Append(spp);
				sb.Append(" +");
				sb.Append(PermSpellIncrease);
				sb.AppendLine(" damage");
			} else if (PermSpellIncrease < 0) {
				//sb.AppendLine(TextFormatter.translateCodes("$4" + spp + " -" + PermSpellIncrease + " damage"));
				sb.Append("$4");
				sb.Append(spp);
				sb.Append(" -");
				sb.Append(PermSpellIncrease);
				sb.AppendLine(" damage");
			}

		}

	}
}