using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JLG.gift.cSharp.background.input;
using UnityEditor;
using JLG.gift.cSharp.entity.player.data;
using JLG.gift.cSharp.buildData;
using JLG.gift.cSharp.SystemData;
using JLG.gift.cSharp.background.video;

namespace JLG.gift.cSharp.background.scene.background {
	public class SceneBackground : MonoBehaviour {

		//static instance of the backgroun
		public static SceneBackground instance;

		//the default bindings
		//public KeyInput defaultKeyBindings;
		//loadscreen canvas
		[SerializeField]
		public GameObject loadScreen;
		[SerializeField]
		public GameObject saveIcon;
		[SerializeField]
		public BuildSelection BuildSelection;
		[SerializeField]
		public GameObject anybutton;
		[SerializeField]
		public GameObject loadInd;

		//should instant load currently selected scene
		//public bool loadSelected = false;
		//public SceneAsset toLoad;

		[HideInInspector]
		public bool loadSaveSceneData;
		[HideInInspector]
		public SceneData saveSceneData;
		[HideInInspector]
		public byte loadId;
		[HideInInspector]
		public bool loadComplete = false;

		//public SceneAsset[] avaliableScenesForLoad;
		//public SceneAsset firstLoad;
		public string firstLoad;

		//curently active scene
		public Scene activeScene {
			get; private set;
		}

		public void putSaveIcon(bool v) {
			saveIcon.SetActive(v);
		}

		//private AsyncOperation currentLoad;
		public void selectBuild(BuildData[] bd) {
			BuildSelection.gameObject.SetActive(true);
			//GameObject con = BuildSelection.contents;

			//foreach (BuildData b in bd) {
			//	BuildSelectionListing bs = GameObject.Instantiate(GlobalItems.instance.BuildSelectionListingPrefab, con.transform).GetComponent<BuildSelectionListing>();
			//}
		}

		public void startLoad(byte num) {
			loadScreen.SetActive(true);
			loadInd.SetActive(true);
			loadSaveSceneData = true;
			new SystemData.SystemData(num);
			//isLoading = true;
			SceneManager.UnloadSceneAsync(MainMenuController.instance.gameObject.scene);
			AsyncHandler.instance.startAsyncTask(startLoadAsync());
		}

		public void startNew(byte num) {
			loadScreen.SetActive(true);
			PlayerData.instance.startNew();
			new SystemData.SystemData(num);
			sendDirect(firstLoad);
			SceneManager.UnloadSceneAsync(MainMenuController.instance.gameObject.scene);
		}

		public void setBackgroundActive() {
			SceneManager.SetActiveScene(gameObject.scene);
		}

		bool saveLoaded = false;
		bool sceneLoaded = false;

		IEnumerator startLoadAsync() {
			saveLoaded = false;
			sceneLoaded = false;
			//foreach (var a in PlayerData.instance.startLoad()) {
			//	yield return null;
			//}
			SystemData.SystemData.loadData.loadFromFileAsync(onSaveLoadComplete);
			while (!saveLoaded) {
				yield return null;
			}

			foreach (var a in PlayerData.instance.startLoad()) {
				yield return null;
			}

			SceneSaveData.loadSceneDataAsync(onSceneLoadComplete, SystemData.SystemData.loadData.playerDat.sceneId);
			//saveSceneData = SaveData.loadedSave.scene;
			//saveSceneData = SceneSaveData.loadSceneDataAsync()
			yield return null;
			while (!sceneLoaded) {
				yield return null;
			}
			sendDirect(saveSceneData.SceneId);
		}

		void onSceneLoadComplete(SceneObjectData[] data) {
			saveSceneData = new SceneData();
			saveSceneData.SceneId = SystemData.SystemData.loadData.playerDat.sceneId;
			saveSceneData.SavePointId = SystemData.SystemData.loadData.playerDat.SavePointId;
			saveSceneData.objectData = data;
			sceneLoaded = true;
			Debug.Log("Scene Loaded Successfully");
		}

		void onSaveLoadComplete() {
			saveLoaded = true;
			Debug.Log("File Loaded Successfully");
		}

		void generateLoadScreen() {
			loadScreen.SetActive(true);
			loadInd.SetActive(true);
		}

		void LoadScreenComplete() {
			loadInd.SetActive(false);
			anybutton.SetActive(true);
			loadComplete = true;
		}

		void LoadScreenClose() {
			loadScreen.SetActive(false);
			loadInd.SetActive(false);
			anybutton.SetActive(false);
			loadComplete = false;
			Time.timeScale = 1;
		}

		// Start is called before the first frame update
		void Start() {
			instance = this;
			loadScreen.SetActive(false);
			loadInd.SetActive(false);
			anybutton.SetActive(false);
			saveIcon.SetActive(false);
			BuildSelection.gameObject.SetActive(false);
		}

		// Update is called once per frame
		void Update() {
			//checks if should instant load
			//if (loadSelected) {
			//	//loads
			//	//sendDirect("_TestScene");
			//	sendDirect(toLoad.name);
			//	loadSelected = false;
			//}
			if (loadComplete && Input.anyKeyDown) {
				SceneManager.SetActiveScene(activeScene);
				LoadScreenClose();
			}
		}

		public void updateFrameRate() {
			Application.targetFrameRate = VideoOptions.instance.targetFps;
		}

		//transfers from current scene to new scene
		public void transferScene(Scene current, string name) {

			//puts loading screen
			SceneManager.SetActiveScene(gameObject.scene);

			//unloads old scene
			SceneManager.UnloadSceneAsync(current);

			//starts loading new scene
			SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

			//sets loadcomplete method
			SceneManager.sceneLoaded += onLoadComplete;

			//turns on loadscreen
			//loadScreen.SetActive(true);
			//loadInd.SetActive(true);
			generateLoadScreen();
		}

		//sends direct from current scene unloading to old after loading
		public void sendDirect(Scene current, string name) {

			SceneManager.SetActiveScene(gameObject.scene);
			SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
			SceneManager.sceneLoaded += onLoadCompleteDirect;
			prevDir = current;
			//loadScreen.SetActive(true);
		}

		//sends direct from current scene without unloading previous
		public void sendDirect(string name) {

			SceneManager.SetActiveScene(gameObject.scene);
			SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
			SceneManager.sceneLoaded += onLoadComplete;
			//prevDir = current;
			//loadScreen.SetActive(true);
		}

		//reloads current scene
		public void reload(Scene name) {
			SceneManager.SetActiveScene(gameObject.scene);
			SceneManager.UnloadSceneAsync(name);
			SceneManager.sceneUnloaded += onReloadUnloadComplete;
		}

		//first method called after unloading for reload
		void onReloadUnloadComplete(Scene scene) {
			SceneManager.sceneUnloaded -= onReloadUnloadComplete;
			SceneManager.LoadSceneAsync(scene.name, LoadSceneMode.Additive);
			SceneManager.sceneLoaded += onLoadComplete;
		}

		//called when scene loading completed
		void onLoadComplete(Scene scene, LoadSceneMode mode) {
			//set active scene
			activeScene = scene;
			//SceneManager.SetActiveScene(scene);
			//remove from scene load method list
			SceneManager.sceneLoaded -= onLoadComplete;

			//loadScreen.SetActive(false);
			//loadInd.SetActive(false);
			//anybutton.SetActive(true);
			//loadComplete = true;
			LoadScreenComplete();
			Time.timeScale = 0;
		}

		//previous scene from a direct load
		Scene prevDir;

		//called when a direct load is complete
		void onLoadCompleteDirect(Scene scene, LoadSceneMode mode) {
			//set active scene
			activeScene = scene;
			SceneManager.SetActiveScene(scene);

			//remove from scene load method list
			SceneManager.sceneLoaded -= onLoadComplete;

			//starts unload previous scene
			SceneManager.UnloadSceneAsync(prevDir);
		}

	}
}