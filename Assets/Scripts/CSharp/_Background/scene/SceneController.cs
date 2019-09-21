using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JLG.gift.cSharp.background.scene.background;
using UnityEngine.SceneManagement;
using JLG.gift.cSharp.enviroment.interactble;
using JLG.gift.cSharp.enviroment.triggerable;

namespace JLG.gift.cSharp.background.scene {
	public class SceneController : MonoBehaviour {

		public static SceneController currentScene;

		//the save id of the scene
		//public abstract int id {
		//	get;
		//}

		public bool enableEntites = true;

		[HideInInspector]
		private SceneData sceneData;

		//all the possible spawn areas for saving
		public SavePoint[] saveSpawns;
		public ISceneObjectData[] SceneObjectStorables;
		public GameObject[] toTriggerOnStart;
		public GameObject EnemyFolder;

		// Start is called before the first frame update
		void Start() {

			//checks if the background scene does not exist
			if (SceneBackground.instance == null) {
				//if so, load it
				SceneManager.LoadSceneAsync("BackgroundScene", LoadSceneMode.Additive);
			}

			currentScene = this;

			if (SceneBackground.instance.loadSaveSceneData) {
				SceneData d = SceneBackground.instance.saveSceneData;

				
				if (d.SavePointId < saveSpawns.Length) {
					entity.Entity.player.transform.position = saveSpawns[d.SavePointId].transform.position;
					GameObject.FindGameObjectWithTag("MainCamera").transform.position = saveSpawns[d.SavePointId].transform.position;
				}

				foreach (SceneObjectData isod in d.objectData) {
					ISceneObjectData objd = SceneObjectStorables[isod.objectId];
					objd.stateData.state = isod.state;
					objd.onDataLoad.Invoke();
				}

				SceneBackground.instance.loadSaveSceneData = false;
			} else {
				if (!(toTriggerOnStart is null) && toTriggerOnStart.Length != 0) {
					foreach (GameObject go in toTriggerOnStart) {
						Triggerable tri = go.GetComponent<Triggerable>();
						if (!(tri is null))
							tri.onTrigger();
					}
				}
			}
			sceneData.SceneId = gameObject.scene.name;
			Debug.Log("Scene Id " + sceneData.SceneId);
		}

		public SceneData getSceneData() {
			sceneData.SceneId = gameObject.scene.name;
			List<SceneObjectData> d = new List<SceneObjectData>();
			foreach (ISceneObjectData isod in SceneObjectStorables) {
				d.Add(isod.stateData);
			}
			sceneData.objectData = d.ToArray();
			return sceneData;
		}

		public void updateSettings() {
			Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		}

	}
}