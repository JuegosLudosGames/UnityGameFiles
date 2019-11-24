using JLG.gift.cSharp.inventory;
using JLG.gift.cSharp.inventory.scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace JLG.UnityEditor.cSharp.inspectors {
	[CustomEditor(typeof(ScriptItem))]
	public class ScriptItemBaseEditor : Editor {

		private bool showScriptProp = false;
		private MonoScript script;

		private void OnEnable() {
			if (target == null)
				return;

			//ScriptItem item = (ScriptItem)target;
			//script = MonoScript.FromScriptableObject((ScriptItemBase)item.script);
			//EditorUtility.SetDirty(this);
		}

		public override void OnInspectorGUI() {

			if (target == null)
				return;

			ScriptItem item = (ScriptItem)target;

			//item.script = (ScriptItemBase) EditorGUILayout.ObjectField("Item Script", item.script, typeof(ScriptItemBase), false);
			base.OnInspectorGUI();
			script = EditorGUILayout.ObjectField("Item Script", item.s, typeof(MonoScript), !EditorUtility.IsPersistent(target)) as MonoScript;
			item.s = script;
			EditorUtility.SetDirty(item);

			if (script != null) {

				if (!script.GetClass().IsSubclassOf(typeof(ScriptItemBase))) {
					EditorGUILayout.LabelField("Invalid ScriptType, Must be a scriptable item");
					return;
				}

				if(item.script == null)
					item.script = Activator.CreateInstance(script.GetClass()) as ScriptItemBase;

				showScriptProp = EditorGUILayout.Foldout(showScriptProp, new GUIContent("Script Properties"));

				if (showScriptProp) {

					if (item.script.GetType() == typeof(ScriptEarthQuakeSpell)) {
						ScriptEarthQuakeSpell s = item.script as ScriptEarthQuakeSpell;
						s.radius = EditorGUILayout.FloatField("Radius", s.radius);
						s.damage = EditorGUILayout.FloatField("Damage", s.damage);
						s.particles = EditorGUILayout.IntField("Particle Amount", s.particles);
						s.particlePreFab = (GameObject) EditorGUILayout.ObjectField("Particle Prefab", s.particlePreFab, typeof(GameObject), !EditorUtility.IsPersistent(target));
						s.delay = EditorGUILayout.FloatField("Delay", s.delay);
					}

					EditorUtility.SetDirty(item.script);
					EditorUtility.SetDirty(item);
					EditorUtility.SetDirty(target);

				}

			}
			EditorUtility.SetDirty(item);
			EditorUtility.SetDirty(this);
		}

	}
}