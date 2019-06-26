using JLG.gift.cSharp.background.scene.background;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.buildData {
	[CreateAssetMenu(fileName = "new BuildData", menuName = "BuildData", order = 60)]
	public class BuildData : ScriptableObject {

		[SerializeField]
		private string bname;
		[SerializeField]
		private Sprite icon;
		[SerializeField]
		private Color iconColor;
		[SerializeField]
		[TextArea]
		private string description;
		[SerializeField]
		private float baseHealthLevel;
		[SerializeField]
		private float baseManaLevel;
		[SerializeField]
		private float baseStrength;
		[SerializeField]
		private float baseMagicStrength;
		[SerializeField]
		private int buildId;

		public string Name { get { return bname; } }
		public float BaseHealthLevel { get { return baseHealthLevel; } }
		public float BaseManaLevel { get { return baseManaLevel; } }
		public float BaseStrength { get { return baseStrength; } }
		public float BaseMagicStrength { get { return baseMagicStrength; } }
		public int BuildId { get { return buildId; } }
		public Sprite Icon { get { return icon; } }
		public Color IconColor { get { return iconColor; } }
		public string Description { get { return description; } }

		public static BuildData getDataFromId(int id) {
			return GlobalItems.instance.BuildDataGet.TryGetValue(id, out BuildData r) ? r : null;
		}

	}
}