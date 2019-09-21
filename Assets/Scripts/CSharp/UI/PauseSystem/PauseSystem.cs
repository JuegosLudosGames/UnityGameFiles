using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JLG.gift.cSharp.ui.menu;
using JLG.gift.cSharp.background.sound;
using JLG.gift.cSharp.background.scene.background;
using JLG.gift.cSharp.formating;
using JLG.gift.cSharp.background.video;
using JLG.gift.cSharp.background.input;
using JLG.gift.cSharp.ui.menuField;

namespace JLG.gift.cSharp.ui.pauseSystem {
	public class PauseSystem : ScreenMenu {

		public GameObject main;
		public GameObject options;
		public FieldHolder options_Vid;
		public FieldHolder options_Ad;
		public FieldHolder options_Con;
		public Image sfxIcon;
		public Image musicIcon;

		private Options sets;

		public bool isPaused {
			get; private set;
		}

		public bool isOptions {
			get; private set;
		}

		// Start is called before the first frame update
		void Start() {
			setActive(options_Vid.gameObject, false);
			setActive(options_Ad.gameObject, false);
			setActive(options_Con.gameObject, false);
			setActive(options, false);
			setActive(main, false);
			
			isPaused = false;
			isOptions = false;
		}

		// Update is called once per frame
		void Update() {
			if (Input.GetKeyDown(KeyCode.Escape)) {
				doEscapeAction();
			}
		}

		private void openOptions() {
			setActive(options, true);
			setActive(main, false);
			isOptions = true;
			loadOptions();
			openMenu(OptionsMenu.VIDEO);
			selectMenu(0);
		}

		private void closeOptions() {
			closeMenu(sets.selected);
			saveOptions();
			MessageBox.instance.messageInfo("Options Saved");
			setActive(options, false);
			setActive(main, true);
			isOptions = false;
		}

		public void selectMenu(int op) {
			OptionsMenu s = (OptionsMenu)op;
			closeMenu(sets.selected);
			openMenu(s);
			closeMenu(sets.selected);
			openMenu(s);
		}

		private void openMenu(OptionsMenu op) {
			switch (op) {
				case OptionsMenu.VIDEO:
					setActive(options_Vid.gameObject, true);
					options_Vid.getAtIndex<bool>(0).setStartValue(sets.displayFps);
					options_Vid.getAtIndex<bool>(1).setStartValue(sets.vsync);
					options_Vid.getAtIndex<int>(2).setStartValue(sets.Target);
					break;
				case OptionsMenu.AUDIO:
					setActive(options_Ad.gameObject, true);
					options_Ad.getAtIndex<int>(0).setStartValue(sets.Volume);
					options_Ad.getAtIndex<int>(1).setStartValue(sets.SfxVolume);
					options_Ad.getAtIndex<int>(2).setStartValue(sets.MVolume);
					options_Ad.getAtIndex<bool>(3).setStartValue(sets.muteS);
					options_Ad.getAtIndex<bool>(4).setStartValue(sets.muteM);
					break;
				case OptionsMenu.CONTROL:
					setActive(options_Con.gameObject, true);
					options_Con.getAtIndex<KeyCode>(0).setStartValue(sets.key.Jump_Button);
					options_Con.getAtIndex<KeyCode>(1).setStartValue(sets.key.Left_Button);
					options_Con.getAtIndex<KeyCode>(2).setStartValue(sets.key.Right_Button);
					options_Con.getAtIndex<KeyCode>(3).setStartValue(sets.key.Physical_Button);
					options_Con.getAtIndex<KeyCode>(4).setStartValue(sets.key.UseItem_Button);
					options_Con.getAtIndex<KeyCode>(5).setStartValue(sets.key.SwitchItemLeft_Button);
					options_Con.getAtIndex<KeyCode>(6).setStartValue(sets.key.SwitchItemRight_Button);
					options_Con.getAtIndex<KeyCode>(7).setStartValue(sets.key.InventoryToggle_Button);
					break;

			}
			sets.selected = op;
			Canvas.ForceUpdateCanvases();
		}

		private void closeMenu(OptionsMenu op) {
			switch (op) {
				case OptionsMenu.VIDEO:
					setActive(options_Vid.gameObject, false);
					sets.displayFps = options_Vid.getAtIndex<bool>(0).getValue();
					sets.vsync = options_Vid.getAtIndex<bool>(1).getValue();
					sets.Target = options_Vid.getAtIndex<int>(2).getValue();
					break;
				case OptionsMenu.AUDIO:
					setActive(options_Ad.gameObject, false);
					sets.Volume = options_Ad.getAtIndex<int>(0).getValue();
					sets.SfxVolume = options_Ad.getAtIndex<int>(1).getValue();
					sets.MVolume = options_Ad.getAtIndex<int>(2).getValue();
					sets.muteS = options_Ad.getAtIndex<bool>(3).getValue();
					sets.muteM = options_Ad.getAtIndex<bool>(4).getValue();
					break;
				case OptionsMenu.CONTROL:
					setActive(options_Con.gameObject, false);
					sets.key.Jump_Button = options_Con.getAtIndex<KeyCode>(0).getValue();
					sets.key.Left_Button = options_Con.getAtIndex<KeyCode>(1).getValue();
					sets.key.Right_Button = options_Con.getAtIndex<KeyCode>(2).getValue();
					sets.key.Physical_Button = options_Con.getAtIndex<KeyCode>(3).getValue();
					sets.key.UseItem_Button = options_Con.getAtIndex<KeyCode>(4).getValue();
					sets.key.SwitchItemLeft_Button = options_Con.getAtIndex<KeyCode>(5).getValue();
					sets.key.SwitchItemRight_Button = options_Con.getAtIndex<KeyCode>(6).getValue();
					sets.key.InventoryToggle_Button = options_Con.getAtIndex<KeyCode>(7).getValue();
					break;
			}
			Canvas.ForceUpdateCanvases();
		}

		private void saveOptions() {
			VideoOptions.instance.displayFps = sets.displayFps;
			VideoOptions.instance.VSync = sets.vsync;
			VideoOptions.instance.targetFps = sets.Target;

			SoundOptions.instance.MasterVolume = sets.Volume;
			SoundOptions.instance.MusicVolume = sets.MVolume;
			SoundOptions.instance.SfxVolume = sets.SfxVolume;
			SoundOptions.instance.Mute(SoundOptions.SoundType.MUSIC, sets.muteM);
			SoundOptions.instance.Mute(SoundOptions.SoundType.SFX, sets.muteS);

			KeyInput.LoadKeySet(sets.key);
			sets = null;
		}

		private void loadOptions() {
			sets = new Options();

			sets.displayFps = VideoOptions.instance.displayFps;
			sets.vsync = VideoOptions.instance.VSync;
			sets.Target = VideoOptions.instance.targetFps;

			sets.Volume = SoundOptions.instance.MasterVolume;
			sets.MVolume = SoundOptions.instance.MusicVolume;
			sets.SfxVolume = SoundOptions.instance.SfxVolume;
			sets.muteM = SoundOptions.instance.musicMute;
			sets.muteS = SoundOptions.instance.sfxMute;

			sets.key = KeyInput.GetActiveKeySet();
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
			if (isOptions) {
				closeOptions();
			} else {
				onPause();
			}
		}

		public void onOptionsSelect() {
			if (isOptions) {
				closeOptions();
			} else {
				openOptions();
			}
		}

		private class Options {

			public OptionsMenu selected;

			//video
			public bool displayFps;
			public bool vsync;
			public int Target;

			//audio
			public int Volume;
			public int MVolume;
			public int SfxVolume;
			public bool muteM;
			public bool muteS;

			//control
			public KeyInput key;
			//jump, walk left, walk right, attack, use, sel left, sel right, inv
		}

		//public enum OptionsMenu {
		//	VIDEO, AUDIO, CONTROL
		//}

	}
}