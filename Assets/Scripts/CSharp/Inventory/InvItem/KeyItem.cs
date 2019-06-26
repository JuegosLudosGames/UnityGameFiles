using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace JLG.gift.cSharp.inventory {
	[CreateAssetMenu(fileName = "new KeyItem", menuName = "KeyItemData", order = 53)]
	public class KeyItem : InvItem {

		[Header("Key Item")]
		[SerializeField]
		[TextArea]
		private string instructionTag;
		[SerializeField]
		private int keyId;

		protected override string addTag {
			get {
				return "$e(Key Item)";
			}
		}

		public string InstructionTag { get { return instructionTag; } }
		public int KeyId { get { return KeyId; } }

		protected override void setTag(StringBuilder sb) {

			sb.AppendLine(instructionTag);

		}

	}
}