using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JLG.gift.cSharp.ui.menu;
using JLG.gift.cSharp.background.sound;
using JLG.gift.cSharp.background.scene.background;
using JLG.gift.cSharp.formating;

namespace JLG.gift.cSharp.ui.pauseSystem {
	public class PauseSystem : ScreenMenu {

		public GameObject main;
		public Image sfxIcon;
		public Image musicIcon;

		public bool isPaused {
			get; private set;
		}

		// Start is called before the first frame update
		void Start() {
			setActive(main, false);
			isPaused = false;
		}

		// Update is called once per frame
		void Update() {
			if (Input.GetKeyDown(KeyCode.Escape)) {
				doEscapeAction();
			}
		}

		public void onPause() {
			if (isPaused) {
				setActive(main, false);
				Time.timeScale = 1;
				isPaused = false;
			} else {
				setActive(main, true);
				Time.timeScale = 0;
				isPaused = true;
				inventory.InventorySystem system = GameObject.FindGameObjectWithTag("InventorySystem").GetComponent<inventory.InventorySystem>();
				if (system.menuActive)
					system.toggleMenu();
			}
		}

		public void onContinue() {
			onPause();
		}

		public void onExit() {
			SceneBackground.instance.transferScene(gameObject.scene, "MainMenu");
		}

		public void onMute() {
			SoundOptions.instance.Mute(SoundOptions.SoundType.SFX, !SoundOptions.instance.sfxMute);
			if (SoundOptions.instance.sfxMute) {
				sfxIcon.sprite = GlobalItems.instance.soundDisabled;
			} else {
				sfxIcon.sprite = GlobalItems.instance.soundEnabled;
			}
		}

		public void onMusicMute() {
			SoundOptions.instance.Mute(SoundOptions.SoundType.MUSIC, !SoundOptions.instance.musicMute);
			if (SoundOptions.instance.musicMute) {
				musicIcon.sprite = GlobalItems.instance.musicDisabled;
			} else {
				musicIcon.sprite = GlobalItems.instance.musicEnabled;
			}
		}

		public void doEscapeAction() {
			if (isPaused) {
				//if(active == main)
				//Debug.Log("working");
				onPause();
			} else {
				onPause();
			}
		}

	}
}