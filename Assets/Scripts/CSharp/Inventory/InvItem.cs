using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using JLG.gift.cSharp.formating;
using JLG.gift.cSharp.background.scene.background;

namespace JLG.gift.cSharp.inventory {
	//[CreateAssetMenu(fileName = "new InvItem", menuName = "InvItemData", order = 53)]
	public class InvItem : ScriptableObject {

		//protected static readonly string incMsg = " +";

		[Header("Technical")]
		[SerializeField]
		private int id;
		[SerializeField]
		private Sprite icon;
		[SerializeField]
		private float pickupScale;
		[SerializeField]
		private string displayName;
		[SerializeField]
		[TextArea]
		private string tag;
		[SerializeField]
		private bool sellable = true;
		[SerializeField]
		private int buyCost;
		[SerializeField]
		private int sellCost;
		[SerializeField]
		[TextArea]
		private string sellDescription;

		protected virtual string addTag { get { return "$e(Item)"; } }

		//[ShowNoEditInInspector()]
		//[TextArea]
		//[SerializeField]
		//private string fullTag;

		public Sprite Icon { get { return icon; } }
		public int Id { get { return id; } }
		public float scale { get { return pickupScale; } }
		public string DisplayName { get { return displayName; } }
		public string Tag { get { return tag; } }
		public bool Sellable { get { return sellable; } }
		public int BuyCost { get { return buyCost; } }
		public int SellCost { get { return sellCost; } }
		public string SellDescription { get { return sellDescription + "\n\n" + addTag; } }
		public string FullTag { get { return getFullTag(); } }

		public static InvItem getAssetById(int id) {
			//Debug.Log("getting item " + id.ToString() + " and recieved " + (GlobalItems.instance.InvItemsGet.TryGetValue(id, out InvItem r2) ? r2 : null).id);
			return GlobalItems.instance.InvItemsGet.TryGetValue(id, out InvItem r) ? r : null;
		}

		private string getFullTag() {
			StringBuilder sb = new StringBuilder();

			sb.AppendLine(DisplayName);
			sb.AppendLine();

			setTag(sb);

			sb.Append(tag);

			//return sb.ToString();
			return TextFormatter.translateCodes(sb.ToString());
		}

		public string getRawFullTag() {
			StringBuilder sb = new StringBuilder();

			sb.AppendLine(DisplayName);
			sb.AppendLine();

			setTag(sb);

			sb.Append(tag);

			return sb.ToString();
			//return TextFormatter.translateCodes(sb.ToString());
		}

		protected virtual void setTag(StringBuilder sb) { }

		public override bool Equals(object other) {
			return base.Equals(other);
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

		public override string ToString() {
			return base.ToString();
		}

		protected static int percent(float val) {
			val *= 100;
			return (int)val;
		}

		//public static bool operator ==(InvItem ls, InvItem rs) {
		//	//Debug.Log("b " + ReferenceEquals(ls, null) + " c " + ReferenceEquals(rs, null));
		//	if ((!(ls is null)) && (!(rs is null))) {
		//		if (ls.id == rs.id) {
		//			return true;
		//		} else {
		//			return false;
		//		}
		//	} else if (ReferenceEquals(ls, null) && ReferenceEquals(rs, null)) {
		//		return true;
		//	} else {
		//		return false;
		//	}
		//}

		//public static bool operator !=(InvItem ls, InvItem rs) {
		//	//if ((!ReferenceEquals(ls, null)) || (!ReferenceEquals(rs, null))) { 
		//	//	if (ls.id != rs.id)
		//	//		return true;
		//	//} else {
		//	//	return true;
		//	//}
		//	//return false;
		//	//return !ls == rs;
		//	if ((!(ls is null)) && (!(rs is null))) {
		//		if (ls.id == rs.id) {
		//			return false;
		//		} else {
		//			return true;
		//		}
		//	} else if (ReferenceEquals(ls, null) && ReferenceEquals(rs, null)) {
		//		return false;
		//	} else {
		//		return true;
		//	}
		//}

	}
}