using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using JLG.gift.cSharp.background.scene.background;
using JLG.gift.cSharp.inventory;

namespace JLG.UnityEditor.cSharp.inspectors {
	[CustomEditor(typeof(GlobalItems))]
	public class GlobalItemsEditor : Editor {

		public override void OnInspectorGUI() {
			base.OnInspectorGUI();

			GlobalItems gi = (GlobalItems)target;

			if (GUILayout.Button("Check Duplicate Item Ids")) {

				Debug.Log("Checking");

				List<int> ids = new List<int>();

				foreach (InvItem i in gi.rawItems) {
					if (ids.Contains(i.Id)) {
						Debug.LogError("Found duplicate: " + i.Id + " with item " + i.name);
					} else {
						ids.Add(i.Id);
					}
				}

				Debug.Log("Done");

			}

		}

	}
}