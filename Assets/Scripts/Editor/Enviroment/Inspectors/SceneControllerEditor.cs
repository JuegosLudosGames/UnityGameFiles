using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using JLG.gift.cSharp.background.scene;
using JLG.gift.cSharp.enviroment.interactble;

namespace JLG.UnityEditor.cSharp.inspectors {
	[CustomEditor(typeof(SceneController))]
	public class SceneControllerEditor : Editor {

		public override void OnInspectorGUI() {
			base.OnInspectorGUI();

			GUILayout.Space(20);

			SceneController sc = (SceneController)target;

			if (GUILayout.Button("Mark and Assign Save Points")) {

				Debug.Log("Searching for savepoints");

				SavePoint[] sp = GameObject.FindObjectsOfType<SavePoint>();
				sc.saveSpawns = sp;

				Debug.Log("Found " + sp.Length + " savepoints");

				for (int x = 0; x < sp.Length; x++) {
					sp[x].id = x;
				}

				Debug.Log("Finished");
			}

			if (GUILayout.Button("Mark and Assign Objects with State")) {
				Debug.Log("Searching for objects");

				ISceneObjectData[] sp = GameObject.FindObjectsOfType<ISceneObjectData>();
				sc.SceneObjectStorables = sp;

				Debug.Log("Found " + sp.Length + " objects");

				for (int x = 0; x < sp.Length; x++) {
					sp[x].stateData.objectId = x;
				}

				Debug.Log("Finished");
			}

		}

	}
}