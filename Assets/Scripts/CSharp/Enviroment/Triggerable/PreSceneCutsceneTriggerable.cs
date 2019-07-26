using JLG.gift.cSharp.background.scene;
using JLG.gift.cSharp.background.scene.background;
using JLG.gift.cSharp.jglScripts.timeline;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace JLG.gift.cSharp.enviroment.triggerable {
	[RequireComponent(typeof(PlayableDirector))]
	public class PreSceneCutsceneTriggerable : ITimeControlInteract, Triggerable {

		public TimelineAsset[] timelines;
		//public TimelineAsset exitTimeLine;
		[HideInInspector]
		public int current;
		[HideInInspector]
		public bool isPlaying = false;
		[HideInInspector]
		public bool isWaiting = false;
		[HideInInspector]
		//public bool isExiting = false;
		public bool shouldContiue = true;

		PlayableDirector director;

		private void Start() {
			director = GetComponent<PlayableDirector>();
			director.stopped += onDirectorStopped;
		}

		private void Update() {
			//is waiting for user input and user gave input
			if (director.state == PlayState.Paused && Input.anyKeyDown) {
				//current++;
				//isWaiting = false;
				////no more timelines are avaliable
				//if (current >= timelines.Length) {
				//	//is there an exit timeline
				//	if (exitTimeLine is null) {
				//		//there isnt
				//		endTime();
				//	} else {
				//		//there is
				//		director.Play(exitTimeLine);
				//	}
				//} else {
				//	//there is more left
				//	director.Play(timelines[current]);
				//}
				director.Resume();
			}
		}

		void endTime() {
			isPlaying = false;
			director.Stop();
			director.playableAsset = null;
			SceneController.currentScene.enableEntites = true;
			Time.timeScale = 1;
		}

		public void onTrigger() {
			if (timelines.Length > 0) {
				SceneController.currentScene.enableEntites = false;
				Time.timeScale = 0;
				current = 0;
				director.Play(timelines[0]);
				isPlaying = true;
			}
		}

		void onDirectorStopped(PlayableDirector d) {
			if (isPlaying && director == d) {
				current++;
				isWaiting = false;
				//no more timelines are avaliable
				if (current >= timelines.Length) {
					endTime();
				} else {
					//there is more left
					director.Play(timelines[current]);
				}
			} 
		}

		public override PlayableDirector getDirector() {
			return director;
		}

		public override bool shouldContinueLoop() {
			return shouldContiue;
		}
	}
}