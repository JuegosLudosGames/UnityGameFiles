using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using JLG.gift.cSharp.background.scene;
using JLG.gift.cSharp.enviroment.interactble;

namespace JLG.UnityEditor.cSharp.inspectors {
	[CustomEditor(typeof(ISceneObjectData), true)]
	public class ISceneObjectDataEditor : Editor {

		public override void OnInspectorGUI() {

			ISceneObjectData obj = (ISceneObjectData)target;

			EditorGUI.BeginDisabledGroup(true);
			EditorGUILayout.IntField("Id", obj.stateData.objectId);
			EditorGUILayout.IntField("Current State", obj.stateData.state);
			EditorGUI.EndDisabledGroup();

			base.OnInspectorGUI();

		}

	}
}