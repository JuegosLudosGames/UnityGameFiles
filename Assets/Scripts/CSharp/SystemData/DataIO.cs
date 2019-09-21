using JLG.gift.cSharp.background.input;
using JLG.gift.cSharp.background.sound;
using JLG.gift.cSharp.background.video;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JLG.gift.cSharp.SystemData {
	[System.Serializable]
	public class DataIO {

		//video settings
		public SerializableKeyInput SerializedkeyBindings;
		public bool displayFPS;
		public int targetFPS;

		//audio settings
		public int MVolume;
		public int SFXVolume;
		public int MuVolume;
		public bool SFXMute;
		public bool MuMute;

		public void loadFromData(KeyInput ki, SoundOptions so, VideoOptions vo) {
			SerializedkeyBindings = ki;
			displayFPS = vo.displayFps;
			targetFPS = vo.targetFps;

			MVolume = so.MasterVolume;
			SFXVolume = so.SfxVolume;
			MuVolume = so.MusicVolume;
			SFXMute = so.sfxMute;
			MuMute = so.musicMute;
		}

		public void saveIntoFileAsync(Action onComplete) {
			SystemData.loadData.database.JsonSaveDataAsync(onComplete, DataBaseIO.SettingJsonPath, this);
		}

		public static void loadFromFileAsync(Action<DataIO> onLoadComplete) {
			SystemData.loadData.database.LoadDataAsync<DataIO>(onLoadComplete, DataBaseIO.SettingJsonPath);
		}

	}
}