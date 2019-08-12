using JLG.gift.cSharp.background.input;
using JLG.gift.cSharp.background.scene;
using JLG.gift.cSharp.background.sound;
using JLG.gift.cSharp.background.video;
using JLG.gift.cSharp.entity.player.data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.SystemData {
	public class SystemData {

		public static SystemData loadData;

		public DataBaseIO database;

		public PlayerSave playerDat;
		public DataIO settingsData;

		public SystemData(byte num) {
			database = new DataBaseIO(num);
			loadData = this;
			//playerDat = new PlayerSave();
			//settingsData = new DataIO();
		}

		Action onComplete;

		public void loadFromFileAsync(Action onComplete) {
			this.onComplete = onComplete;
			PlayerSave.loadFromFileAsync(onPlayerLoadComplete);
		}

		void onPlayerLoadComplete(PlayerSave s) {
			playerDat = s;
			DataIO.loadFromFileAsync(onSettingsLoadComplete);
		}

		void onSettingsLoadComplete(DataIO d) {
			settingsData = d;
			onComplete();
		}

		public void SaveToFileAsync(PlayerData pd, SceneData data, SoundOptions so, VideoOptions vo, Action onComplete) {
			playerDat = new PlayerSave();
			playerDat.loadFromData(pd, data.SavePointId, data.SceneId);
			settingsData = new DataIO();
			settingsData.loadFromData(pd.keybinding, so, vo);

			this.onComplete = onComplete;
			SceneSaveData.saveSceneDataAsync(data.objectData, onSceneSaveComplete, data.SceneId);
		}

		void onSceneSaveComplete() {
			playerDat.saveIntoFileAsync(onPlayerSaveComplete);
		}

		void onPlayerSaveComplete() {
			settingsData.saveIntoFileAsync(onSettingSaveComplete);
		}

		void onSettingSaveComplete() {
			onComplete();
		}

	}
}