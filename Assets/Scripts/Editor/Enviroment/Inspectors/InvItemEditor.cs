using UnityEngine;
using UnityEditor;
using JLG.gift.cSharp.inventory;

namespace JLG.UnityEditor.cSharp.inspectors {
	[CustomEditor(typeof(InvItem), true)]
	public class InvItemEditor : Editor {
		public override void OnInspectorGUI() {
			base.OnInspectorGUI();

			InvItem item = (InvItem)target;

			EditorGUI.BeginDisabledGroup(true);

			GUILayout.Label("Tooltip Display Tag");
			EditorGUILayout.TextArea(item.getRawFullTag());

			if (item.Sellable) {
				GUILayout.Label("Merchant Description Tag");
				EditorGUILayout.TextArea(item.SellDescription);
			} 

			EditorGUI.EndDisabledGroup();
		}
	}
}