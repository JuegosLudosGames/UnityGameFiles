using JLG.gift.cSharp.background.scene.background;
using JLG.gift.cSharp.entity.player.data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace JLG.gift.cSharp.background.scene {
	public class MainMenuController : MonoBehaviour {

		public static MainMenuController instance;

		public GameObject main;
		public GameObject saveMenu;
		public GameObject creditsMenu;
		public GameObject delConfirm;
		public GameObject overConfirm;

		[Header("SaveGame Things")]
		public GameObject slot1Button;
		public GameObject slot2Button;
		public GameObject slot3Button;
		public GameObject deleteButton;
		public Color fullColor;
		public Color emptyColor;
		public Color deletesetcolor;

		[Header("Start Game")]
		public Scene startScene;

		private Color prevDelColor;

		bool isNewGame = true;
		bool isDeleting = false;

		GameObject active;

		private void Start() {
			instance = this;
			goToMain();
			delConfirm.SetActive(false);
			overConfirm.SetActive(false);
			SceneManager.LoadSceneAsync("BackgroundScene", LoadSceneMode.Additive); 
		}

		//main Screen
		public void onNewGame() {
			isNewGame = true;
			goToSaveSel();
		}

		public void onContinueGame() {
			isNewGame = false;
			goToSaveSel();
		}

		public void onCredits() {
			main.SetActive(false);
			saveMenu.SetActive(false);
			creditsMenu.SetActive(true);
		}

		public void onQuit() {
			Application.Quit();
		}

		private void goToSaveSel() {
			main.SetActive(false);
			saveMenu.SetActive(true);
			creditsMenu.SetActive(false);
			updateSave();
		}

		private void goToDelConfirm() {
			main.SetActive(false);
			saveMenu.SetActive(false);
			creditsMenu.SetActive(false);
			delConfirm.SetActive(true);
		}

		private void goToOverrideConfirm() {
			main.SetActive(false);
			saveMenu.SetActive(false);
			creditsMenu.SetActive(false);
			overConfirm.SetActive(true);
		}

		private void startGameAtBeg() {
			Debug.Log("Starting new");
			SceneBackground.instance.setBackgroundActive();
			SceneBackground.instance.startNew();
		}

		private void startGameAtLoad() {
			Debug.Log("Starting continue");
			//SceneBackground.instance.startLoad();
			//SceneBackground.instance;
			SceneBackground.instance.setBackgroundActive();
			SceneBackground.instance.loadScreen.SetActive(true);
			SaveData.loadDataAsync(toCon, onSaveDataLoadComplete);
		}

		void onSaveDataLoadComplete() {
			SceneBackground.instance.startLoad();
		}

		public void updateSave() {

			if (SaveData.doesSaveExist(1)) {
				slot1Button.GetComponent<Image>().color = fullColor;
				slot1Button.GetComponentInChildren<Text>().text = "Slot 1: full";
			} else {
				slot1Button.GetComponent<Image>().color = emptyColor;
				slot1Button.GetComponentInChildren<Text>().text = "Slot 1: empty";
			}

			if (SaveData.doesSaveExist(2)) {
				slot2Button.GetComponent<Image>().color = fullColor;
				slot2Button.GetComponentInChildren<Text>().text = "Slot 2: full";
			} else {
				slot2Button.GetComponent<Image>().color = emptyColor;
				slot2Button.GetComponentInChildren<Text>().text = "Slot 2: empty";
			}

			if (SaveData.doesSaveExist(3)) {
				slot3Button.GetComponent<Image>().color = fullColor;
				slot3Button.GetComponentInChildren<Text>().text = "Slot 3: full";
			} else {
				slot3Button.GetComponent<Image>().color = emptyColor;
				slot3Button.GetComponentInChildren<Text>().text = "Slot 3: empty";
			}

		}

		public void goToMain() {
			main.SetActive(true);
			saveMenu.SetActive(false);
			creditsMenu.SetActive(false);
		}

		//level select Screen
		public void onSaveSelect(int a) {
			byte b = (byte) a;

			if (!isDeleting) {
				if (isNewGame) {
					if (SaveData.doesSaveExist(b)) {
						//start new 
						toCon = b;
						goToOverrideConfirm();
					} else {
						//SaveData.createNew(b);
						startGameAtBeg();
					}
				} else {
					if (SaveData.doesSaveExist(b)) {
						toCon = b;
						startGameAtLoad();
					}
				}
			} else {
				setDeleting(false);
				toCon = b;
				goToDelConfirm();
			}
		}

		public void toggleDeleting() {
			setDeleting(!isDeleting);
		}

		private void setDeleting(bool s) {
			isDeleting = s;
			if (s) {
				prevDelColor = deleteButton.GetComponent<Image>().color;
				deleteButton.GetComponent<Image>().color = deletesetcolor;
			} else {
				deleteButton.GetComponent<Image>().color = prevDelColor;
			}
		}

		byte toCon;

		public void delYes() {
			SaveData.deleteSave(toCon);
			delConfirm.SetActive(false);
			goToSaveSel();
		}

		public void delNo() {
			delConfirm.SetActive(false);
			goToSaveSel();
		}

		public void overYes() {
			SaveData.deleteSave(toCon);
			SaveData.createNew(toCon);
			overConfirm.SetActive(false);
			startGameAtBeg();
		}

		public void overNo() {
			overConfirm.SetActive(false);
			goToSaveSel();
		}

	}
}