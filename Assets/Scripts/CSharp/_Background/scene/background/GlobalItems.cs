using JLG.gift.cSharp.background.input;
using JLG.gift.cSharp.inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.background.scene.background {
	public class GlobalItems : MonoBehaviour {

		public static GlobalItems instance;

		[Header("Default Prefabs")]
		public GameObject groundItemPrefab;
		public GameObject ItemLockItemPrefab;
		public GameObject ItemLockItemMorePrefab;
		public GameObject ItemListingPrefab;
		public GameObject BuildSelListingPrefab;
		public GameObject BuildSelectionListingPrefab;

		[Header("AssetRef")]
		[SerializeField]
		private InvItem[] items;
		public InvItem[] rawItems { get => items; }

		[Header("BuildRed")]
		[SerializeField]
		private buildData.BuildData[] builds;

		[Header("Sound options")]
		public Sprite soundEnabled;
		public Sprite soundDisabled;
		public Sprite musicEnabled;
		public Sprite musicDisabled;

		[Header("Keybindings")]
		public KeyInput defaultBinding;

		public Dictionary<int, InvItem> InvItemsGet;
		public Dictionary<int, buildData.BuildData> BuildDataGet;

		private void Start() {
			instance = this;
			InvItemsGet = new Dictionary<int, InvItem>();
			for (int x = 0; x < items.Length; x++) {
				if (!InvItemsGet.ContainsKey(items[x].Id)) {
					InvItemsGet.Add(items[x].Id, items[x]);
				} else {
					Debug.LogWarning("Duplicate Inv Id: 1:" + items[x].name + " 2: " + InvItemsGet[items[x].Id].name + " id: " + items[x].Id);
				}
			}

			BuildDataGet = new Dictionary<int, buildData.BuildData>();
			for (int x = 0; x < builds.Length; x++) {
				if (!BuildDataGet.ContainsKey(builds[x].BuildId)) {
					BuildDataGet.Add(builds[x].BuildId, builds[x]);
				} else {
					Debug.LogWarning("Duplicate build Id: 1:" + builds[x].Name + " 2: " + BuildDataGet[builds[x].BuildId].Name + " id: " + builds[x].BuildId);
				}
			}

		}

	}
}