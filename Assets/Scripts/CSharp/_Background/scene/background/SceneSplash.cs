using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JLG.gift.cSharp.background.scene.background {
	public class SceneSplash : MonoBehaviour {
		// Start is called before the first frame update
		void Start() {
			SceneManager.LoadSceneAsync("BackgroundScene", LoadSceneMode.Additive);
			SceneManager.sceneLoaded += onSceneCompleteFirst;
		}

		private void onSceneCompleteFirst(Scene scene, LoadSceneMode mode) {
			SceneManager.sceneLoaded -= onSceneCompleteFirst;
			SceneManager.sceneLoaded += onSceneCompleteSecond;
			SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
		}

		private void onSceneCompleteSecond(Scene scene, LoadSceneMode mode) {
			SceneManager.sceneLoaded -= onSceneCompleteSecond;
			SceneManager.SetActiveScene(scene);
			SceneManager.UnloadSceneAsync(this.gameObject.scene);
		}
		
	}
}