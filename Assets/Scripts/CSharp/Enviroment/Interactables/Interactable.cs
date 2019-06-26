using JLG.gift.cSharp.entity;
using JLG.gift.cSharp.entity.player.data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.enviroment.interactble {
	public abstract class Interactable : MonoBehaviour {

		public static HashSet<Interactable> currentPlayerRange = new HashSet<Interactable>();

		//range in which will activate the interactable
		public float activateRange;
		//should the interactable start enabled
		public bool isStartingEnabled;

		//enables and disables the interactable
		public bool enable {
			//getter
			get { return isEnabled; }
			//setter
			set {
				//are they different, if not skip
				if (isEnabled != value) {
					//set value
					isEnabled = value;
					//if enabling, do enable action, otherwise disable actions
					if (value)
						onEnable();
					else
						onDisable();
				}
			}
		}

		//enables and disables the range component of interactable
		public bool rangeEnable {
			get { return isRangeEnabled; }
			set {
				if (isRangeEnabled != value) {
					isRangeEnabled = value;
					if (value)
						onRangeEnable();
					else
						onRangeDisable();
				}
			}
		}

		//is ranage currently enabled
		private bool isRangeEnabled;
		//are we currently enabled
		private bool isEnabled;
		//was the player in range last frame
		private bool inRangeLastFrame;

		private void Start() {
			isEnabled = isStartingEnabled;

			onStart();

			if (isStartingEnabled)
				onEnable();
			else
				onDisable();
			
		}

		private void Update() {
			onUpdate();
		}

		protected virtual void onUpdate() {
			if (isRangeEnabled) {
				if (inRangeLastFrame) {
					if (isPlayerInRange()) {
						whenStayRange();
					} else {
						whenLeaveRange();
						inRangeLastFrame = false;
					}
				} else if (isPlayerInRange()) {
					whenEnterRange();
					inRangeLastFrame = true;
				}
			}
		}

		public void Interact() {
			if(isEnabled) 
				onInteract();
		}

		protected bool isPlayerInRange() {
			return Vector2.Distance(Entity.player.transform.position, transform.position) <= activateRange;
		}

		protected virtual void onStart() { }
		protected abstract void onInteract();
		protected virtual void whenEnterRange() {
			currentPlayerRange.Add(this);
		}		
		protected virtual void whenLeaveRange() {
			if (currentPlayerRange.Contains(this))
				currentPlayerRange.Remove(this);
		}
		protected virtual void whenStayRange() {}
		protected virtual void onDisable() {}
		protected virtual void onEnable() {}
		protected virtual void onRangeDisable() {
			if (currentPlayerRange.Contains(this))
				currentPlayerRange.Remove(this);
		}
		protected virtual void onRangeEnable() { }
		
	}
}