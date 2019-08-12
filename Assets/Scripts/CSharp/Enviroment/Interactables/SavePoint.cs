using JLG.gift.cSharp.background.scene;
using JLG.gift.cSharp.background.scene.background;
using JLG.gift.cSharp.background.sound;
using JLG.gift.cSharp.background.video;
using JLG.gift.cSharp.entity.player.data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.enviroment.interactble {
	public class SavePoint : Interactable {

		public SceneController scene {
			get; private set;
		}

		public GameObject popup;

		public int id;

		protected override void onStart() {
			base.onStart();
			popup.SetActive(false);
			rangeEnable = true;
			scene = SceneController.currentScene;
		}

		protected override void onInteract() {
			SceneBackground.instance.putSaveIcon(true);
			SceneData sd = SceneController.currentScene.getSceneData();
			sd.SavePointId = id;
			//new SaveData(PlayerData.instance, sd).saveDataAsync(1, onSaveComplete);

			SystemData.SystemData.loadData.SaveToFileAsync(PlayerData.instance, sd, SoundOptions.instance, VideoOptions.instance, onSaveComplete);
		}

		void onSaveComplete() {
			SceneBackground.instance.putSaveIcon(false);
			MessageBox.instance.messageInfo("Game Saved!");
		}

		protected override void whenLeaveRange() {
			base.whenLeaveRange();
			popup.SetActive(false);
		}

		protected override void whenEnterRange() {
			base.whenEnterRange();
			popup.SetActive(true);
		}


	}
}