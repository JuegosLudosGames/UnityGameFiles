using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using JLG.gift.cSharp.inventory;
using JLG.gift.cSharp.background.input;
using JLG.gift.cSharp.background;
using JLG.gift.cSharp.background.scene.background;
using JLG.gift.cSharp.background.scene;

namespace JLG.gift.cSharp.entity.player.data {
	[Serializable]
	public class SaveData {

		public static SaveData loadedSave;

		public SerializableInventory Inventory;
		public SerializableKeyInput Keybinding;
		public float StrengthLevelIncrease;
		public float SpellStrengthLevelIncrease;
		public float MaxHealth;
		public float MaxMana;
		public int buildId;
		public SceneData scene;

		public SaveData(PlayerData data, SceneData sdata) {
			Inventory = data.inventory;
			Keybinding = data.keybinding;
			StrengthLevelIncrease = data.strengthInc;
			SpellStrengthLevelIncrease = data.spellInc;
			MaxHealth = data.maxHealthAdd;
			MaxMana = data.maxManaAdd;
			buildId = data.buildData.BuildId;
			scene = sdata;
		}

		public SaveData() { }

		public void saveDataAsync(byte saveNum, Action onFinish) {
			temp_saveNum = saveNum;
			this.act = onFinish;
			AsyncHandler.instance.startAsyncTask(saveDataCo());
		}

		void saveDirect(byte saveNum) {
			string path = Path.Combine(Application.persistentDataPath, "save" + temp_saveNum + ".json");
			string rawJson = JsonUtility.ToJson(this);
			using (StreamWriter writer = File.CreateText(path)) {
				writer.Write(rawJson);
			}
			if (!(act is null)) {
				act.Invoke();
			}
		}

		byte temp_saveNum;
		Action act;

		IEnumerator saveDataCo() {
			string path = Path.Combine(Application.persistentDataPath, "save" + temp_saveNum + ".json");
			string rawJson = JsonUtility.ToJson(this);
			yield return null;
			using (StreamWriter writer = File.CreateText(path)) {
				writer.Write(rawJson);
			}
			yield return null;
			if(!(act is null))
				act.Invoke();
		}

		public void saveAndUnload(byte saveNum) {
			//saveData(saveNum);
			saveDataAsync(saveNum, unLoad);
			unLoad();
		}

		//void onSaveFinish() {
		//	unLoad();
		//}

		public void unLoad() {
			loadedSave = null;
		}

		//public static SaveData loadData(int saveNum) {
		//	string path = Path.Combine(Application.persistentDataPath, "save" + saveNum + ".json");

		//	if (!doesSaveExist(saveNum))
		//		throw new FileNotFoundException();

		//	using (StreamReader reader = File.OpenText(path)) {
		//		string rawJson = reader.ReadToEnd();
		//		loadedSave = JsonUtility.FromJson<SaveData>(rawJson);
		//		return loadedSave;
		//	}
		//}

		public static void loadDataAsync(byte saveNum, Action onLoadComplete) {
			SaveData.saveNum = saveNum;
			SaveData.onLoadComplete = onLoadComplete;
			AsyncHandler.instance.startAsyncTask(loadDataCo());
		}

		static Action onLoadComplete;
		static byte saveNum;

		static IEnumerator loadDataCo() {
			string path = Path.Combine(Application.persistentDataPath, "save" + saveNum + ".json");

			if (!doesSaveExist(saveNum))
				throw new FileNotFoundException(saveNum.ToString());

			yield return null;

			using (StreamReader reader = File.OpenText(path)) {
				string rawJson = reader.ReadToEnd();
				yield return null;
				loadedSave = JsonUtility.FromJson<SaveData>(rawJson);
			}
			yield return null;
			onLoadComplete();
		}

		public static SaveData createNew(byte saveNum) {
			SaveData data = new SaveData();

			data.Inventory = new SerializableInventory(new List<ItemStack>(), new List<ItemStack>(), new List<ItemStack>(), 0);
			data.Keybinding = GlobalItems.instance.defaultBinding;
			data.StrengthLevelIncrease = 0;
			data.SpellStrengthLevelIncrease = 0;
			data.MaxMana = 0;
			data.MaxHealth = 0;

			//data.saveData(saveNum);
			//data.saveDirect(saveNum);
			data.saveDataAsync(saveNum, null);

			return data;
		}

		public static void deleteSave(byte saveNum) {
			if (doesSaveExist(saveNum)) {
				File.Delete(Path.Combine(Application.persistentDataPath, "save" + saveNum + ".json"));
			}
		}

		public static bool doesSaveExist(byte saveNum) {
			string path = Path.Combine(Application.persistentDataPath, "save" + saveNum + ".json");
			return File.Exists(path);
		}

	}
}