using JLG.gift.cSharp.background.scene;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.SystemData {
	public static class SceneSaveData {

		public static void loadSceneDataAsync(Action<SceneObjectData[]> onComplete, string sceneId) {
			SystemData.loadData.database.readSceneTableAsync(sceneId, onComplete);
		}

		public static void saveSceneDataAsync(SceneObjectData[] objects, Action onComplete, string sceneId) {
			SystemData.loadData.database.updateSceneTableAsync(sceneId, objects, onComplete);
		}

	}
}