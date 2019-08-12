using JLG.gift.cSharp.entity.player.data;
using JLG.gift.cSharp.inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.SystemData {
	[System.Serializable]
	public class PlayerSave {
		public SerializableInventory Inventory;
		public float StrengthLevelIncrease;
		public float SpellStrengthLevelIncrease;
		public float MaxHealth;
		public float MaxMana;
		public int BuildId;
		public int SavePointId;
		public string sceneId;

		public void loadFromData(PlayerData dat, int saveP, string sceneId) {
			Inventory = dat.inventory;
			StrengthLevelIncrease = dat.strengthInc;
			SpellStrengthLevelIncrease = dat.spellInc;
			MaxHealth = dat.maxHealthAdd;
			MaxMana = dat.maxManaAdd;
			BuildId = dat.buildData.BuildId;
			SavePointId = saveP;
			this.sceneId = sceneId;
		}

		public void saveIntoFileAsync(Action onComplete) {
			SystemData.loadData.database.JsonSaveDataAsync(onComplete, DataBaseIO.PlayerJsonPath, this);
		}

		public static void loadFromFileAsync(Action<PlayerSave> onLoadComplete) {
			SystemData.loadData.database.LoadDataAsync<PlayerSave>(onLoadComplete, DataBaseIO.PlayerJsonPath);
		}
	}
}