using JLG.gift.cSharp.background.scene;
using JLG.gift.cSharp.entity.player;
using JLG.gift.cSharp.enviroment.triggerable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace JLG.gift.cSharp.entity.bosses {

	public class Boss : Entity, Triggerable {

		//status
		public bool hasStarted = false;             //if the boss fight has started
		private int IState = 0;						//the fight stage number
		public int state {                          //the fight stage number related property
			get {
				return IState;						//on get, return state value
			}
			set {
				IState = value;						//on set, trigger change state method
				onStateChange();
			}
		}
		protected virtual void onStateChange() { }	//method called when state change occurs
		private ISceneObjectData isod;              //the saveData of the object (MUST be seperate from boss object)
		public bool invulnerable = false;			//weather or not the boss is currently invulnerable

		//start objects
		public bool isStarting = false;				//if start scripts are running
		public bool shouldPlayStart = false;		//if weather or not start scripts should be run
		public bool shouldCameraStay = true;		//if the camera should stay in one place
		public GameObject cameraStayReference;		//weather the camera should stay in a specific reference
		public PlayableAsset preStart;              //the timeline for starting

		//end objects
		public bool isEnding = false;				//if end scripts are running
		public bool shouldPlayEnd = false;			//if the end should be played
		public PlayableAsset preEnd;				//the timeline for ending

		//operational objects/components
		public PlayableDirector director;			//the playable director

		//when the object starts
		protected override void onStart() {
			camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowPlayer>();
		}

		private FollowPlayer camera;				//the camera object

		//when the object data is loaded
		public void onDataLoad() {
			if (isod.stateData.state == 1) { //when the object load state is completed
				GameObject.Destroy(gameObject);	//destroy the object
			}
		}

		//when the fight is triggered
		public void onTrigger() {
			//should we run scripts
			if (shouldPlayStart) {
				//run scripts
				isStarting = true;
				director.playableAsset = preStart;
				if (shouldCameraStay) {
					camera.shouldFollowOtherObject = true;
					camera.otherObject = cameraStayReference;
				}
				director.Play();
			}
		}

		//when end comes
		public void triggerEnd() {
			if (shouldPlayEnd) {				// play scripts?
				//ends scripts
				isEnding = true;
				director.playableAsset = preEnd;
				director.Play();
			} else {
				//otherwise straight up end
				endAll();
			}
		}

		//when the fight starts
		public virtual void onFightStart() {

		}

		public virtual void onFightEnd() {

		}

		//ends all the object
		private void endAll() {
			isod.stateData.state = 1;				//sets state to complete
			camera.shouldFollowOtherObject = false; //stops camera from being stationary
			GameObject.Destroy(gameObject);			//destroy object
		}

		protected override void onEarlyUpdate() {
			if (isStarting) { //is the fight still on start scripts
				if (director.state == PlayState.Paused) { //did it finish start scripts
					//start the fight
					isStarting = false;
					onFightStart();
				}
			} else if(isEnding) {
				if (director.state == PlayState.Paused) { //did it finish end scripts
					//end the fight
					isEnding = false;
					onFightEnd();
					endAll();
				}
			}
		}

		public override void damage(float damage) {
			if (!invulnerable) {
				//remove from health
				health -= damage;

				modelAnimator.SetTrigger("Damaged");
				//should it be dead?
				if (health < 0) {
					//kill it
					triggerEnd();
					return;
				}
			}
		}

	}
}