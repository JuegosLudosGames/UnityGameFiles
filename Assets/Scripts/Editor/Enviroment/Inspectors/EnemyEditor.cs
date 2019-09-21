using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using JLG.gift.cSharp.entity.enemy;
using JLG.gift.cSharp.entity;
using JLG.gift.cSharp.attackData.spells.spelltype;
using JLG.gift.cSharp.entity.ai.aiPackets;

namespace JLG.UnityEditor.cSharp.inspectors {
	[CustomEditor(typeof(Enemy))]
	public class EnemyEditor : Editor {

		private bool showDebug = false;
		private bool showDebugCombat = false;
		private bool showDebugBase = false;
		private bool showPhysics = true;
		private bool arrayOpenGround = false;
		private bool showStats = true;
		private bool showAttack = true;
		private bool showAi = true;
		private bool showAiPersonaData = false;
		private bool showReferences = true;

		private SerializedObject serializedobj;
		private SerializedProperty groundc;

		private void OnEnable() {
			serializedobj = new SerializedObject(target);
			groundc = serializedobj.FindProperty("groundChecks");
		}

		public override void OnInspectorGUI() {
			//base.OnInspectorGUI();

			//GUILayout.Label("Debug", EditorStyles.boldLabel);

			if (target is null)
				return;

			Enemy e = (Enemy)target;

			showDebug = EditorGUILayout.Foldout(showDebug, "Debug Data");

			if (showDebug) {

				EditorGUI.indentLevel++;

				showDebugBase = EditorGUILayout.Foldout(showDebugBase, "Basic Data");

				if (showDebugBase) {

					EditorGUI.indentLevel++;

					EditorGUI.BeginDisabledGroup(true);

					EditorGUILayout.Toggle("Enabled", e.isEnabled);
					EditorGUILayout.Space();
					EditorGUILayout.Toggle("Grounded", e.grounded);
					EditorGUILayout.Space();
					EditorGUILayout.Toggle("Despawning", e.isDespawning);
					EditorGUILayout.FloatField("Despawn Time", e.timeToDespawn);
					EditorGUILayout.Space();
					EditorGUILayout.Toggle("ConLookingCurDir", e.continueLookingdir);
					EditorGUILayout.Space();
					EditorGUILayout.IntField("MaxColliderCheck", Entity.maxColliderTest);
					EditorGUILayout.Space();
					EditorGUILayout.FloatField("Movement in X", e.movementX);
					EditorGUILayout.FloatField("Force in Y", e.forceY);
					EditorGUILayout.TextField("Facing Direction", (e.isLookingInDirection()) ? "Right" : "Left");
					EditorGUILayout.Vector2Field("Spawnpoint", e.spawnPoint);
					EditorGUILayout.Space();

					EditorGUI.EndDisabledGroup();

					EditorGUI.indentLevel--;
				}

				showDebugCombat = EditorGUILayout.Foldout(showDebugCombat, "Combat Data");

				if (showDebugCombat) {
					EditorGUI.indentLevel++;
					EditorGUI.BeginDisabledGroup(true);

					GUILayout.Label("Bars");
					EditorGUILayout.Slider("Health", e.health, 0, e.maxHealth);
					EditorGUILayout.Slider("Mana", e.mana, 0, e.maxMana);
					EditorGUILayout.Space();
					EditorGUILayout.FloatField("AttackDelay", e.timeBeforeNextAttack);
					EditorGUILayout.FloatField("Spell1Delay", e.timeBeforeNextSpell_1);
					EditorGUILayout.FloatField("Spell2Delay", e.timeBeforeNextSpell_2);
					EditorGUILayout.Space();
					EditorGUILayout.Toggle("Player Spotted", e.playerSpotted);
					EditorGUILayout.Toggle("Player Was Spotted", e.playerWasSpotted);
					EditorGUILayout.Toggle("Player Spotted", e.playerSpotted);
					EditorGUILayout.Vector2Field("Last Spotting", e.lastSpotting);
					EditorGUILayout.Space();
					EditorGUILayout.FloatField("TimeToForget", e.timeToForget);
					EditorGUILayout.Space();
					EditorGUILayout.Toggle("Was Damaged", e.wasDamaged);
					EditorGUILayout.FloatField("Damaged Cooldown", e.damagedCool);
					EditorGUILayout.Space();
					EditorGUILayout.TextArea("Packet Running", e.curPack == null? "None" : e.curPack.ToString());
					EditorGUILayout.Space();
					EditorGUILayout.TextArea("State Running", e.curState == null ? "None" : e.curState.ToString());
					EditorGUILayout.Space();

					EditorGUI.EndDisabledGroup();
					EditorGUI.indentLevel--;
				}

				EditorGUI.indentLevel--;

			}

			showPhysics = EditorGUILayout.Foldout(showPhysics, "Physics Data");

			if (showPhysics) {
				EditorGUI.indentLevel++;

				e.horizontalDrag = EditorGUILayout.FloatField("Horizonal Drag", e.horizontalDrag);
				e.verticalDrag = EditorGUILayout.FloatField("Vertical Drag", e.verticalDrag);
				e.fallMultiplier = EditorGUILayout.FloatField("Fall Multiplier", e.fallMultiplier);
				e.SmoothingX = EditorGUILayout.FloatField("Smoothing X", e.SmoothingX);
				e.whatIsGround = EditorGUILayout.LayerField("Ground Layer", e.whatIsGround);
				e.groundedRadius = EditorGUILayout.FloatField("Groundcheck radius", e.groundedRadius);

				EditorGUILayout.PropertyField(groundc, new GUIContent("Ground check objects"), true);

				EditorGUILayout.Space();

				EditorGUI.indentLevel--;
			}

			showStats = EditorGUILayout.Foldout(showStats, "Stats Data");

			if (showStats) {
				EditorGUI.indentLevel++;

				e.maxHeight = EditorGUILayout.FloatField("Max Jump Height", e.maxHeight);
				e.speed = EditorGUILayout.FloatField("Speed", e.speed);
				e.jumpStrength = EditorGUILayout.FloatField("Jump Strength", e.jumpStrength);
				EditorGUILayout.Space();
				e.despawnTime = EditorGUILayout.FloatField("Despawn Time", e.despawnTime);
				e.maxHealth = EditorGUILayout.FloatField("Max Health", e.maxHealth);
				e.maxMana = EditorGUILayout.FloatField("Max Mana", e.maxMana);
				EditorGUILayout.Space();
				e.viewRange = EditorGUILayout.FloatField("View Range", e.viewRange);
				e.viewAngle = EditorGUILayout.FloatField("View Angle", e.viewAngle);
				e.gapCheck = EditorGUILayout.FloatField("Gap Wall Check", e.gapCheck);
				EditorGUILayout.Space();

				EditorGUI.indentLevel--;
			}

			showAttack = EditorGUILayout.Foldout(showAttack, "Attack Data");

			if (showAttack) {
				EditorGUI.indentLevel++;

				e.slashAttackDamage = EditorGUILayout.FloatField("Slash Attack Damage", e.slashAttackDamage);
				e.slashAttackTime = EditorGUILayout.FloatField("Slash Attack Time", e.slashAttackTime);
				EditorGUILayout.Space();
				e.magic1 = (Spell) EditorGUILayout.ObjectField("Magic 1", e.magic1, typeof(Spell), !EditorUtility.IsPersistent(target)); 
				e.magic2 = (Spell) EditorGUILayout.ObjectField("Magic 2", e.magic2, typeof(Spell), !EditorUtility.IsPersistent(target));
				EditorGUILayout.Space();

				EditorGUI.indentLevel--;
			}

			showAi = EditorGUILayout.Foldout(showAi, "Ai Data");

			if (showAi) {
				EditorGUI.indentLevel++;

				e.baseDifficulty = EditorGUILayout.FloatField("Base Difficulty", e.baseDifficulty);
				e.aiDelay = EditorGUILayout.FloatField("Ai Delay", e.aiDelay);
				EditorGUILayout.Space();
				e.persona = (Enemy.Personality.Personalities) EditorGUILayout.EnumPopup("Personality", e.persona);
				EditorGUILayout.Space();

				EditorGUI.indentLevel--;
			}

			showReferences = EditorGUILayout.Foldout(showReferences, "Reference Data");

			if (showReferences) {
				EditorGUI.indentLevel++;

				e.hitBox = (Collider2D)EditorGUILayout.ObjectField("Hit Box", e.hitBox, typeof(Collider2D), !EditorUtility.IsPersistent(target));
				e.slashAttackRange = (Collider2D)EditorGUILayout.ObjectField("SlashAttackRange", e.slashAttackRange, typeof(Collider2D), !EditorUtility.IsPersistent(target));
				EditorGUILayout.Space();

				EditorGUI.indentLevel--;
			}

			//serializedObject.ApplyModifiedProperties();
			if (GUI.changed) {
				EditorUtility.SetDirty(e);
			}

		}
	}
}