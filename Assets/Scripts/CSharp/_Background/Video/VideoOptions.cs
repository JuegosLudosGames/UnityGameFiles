using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JLG.gift.cSharp.background.video {
	public class VideoOptions : MonoBehaviour {

		public static VideoOptions instance;
		public static float timeInterval = 0.25f; //how often the min fps counter will be updated
		public static float timeIntervalAv = 2; //how often the av fps counter will be updated

		private float accum; //total amount of time accumulated over time (min)
		private float accumAv; //total amount of time accumulated over time (Av)
		private float timeLeft; //time before next fps update (min)
		private float timeLeftAv; //time before next fps update (Av)
		private int frames; //counted frames
		private int framesAv; //counted frames (av)
		private int prevCount;
		private int prevCountAv;


		public Text FpsCount;

		[SerializeField]
		private bool DisplayFps = false;
		public bool displayFps {
			get {
				return DisplayFps;
			}
			set {
				DisplayFps = value;
				FpsCount.gameObject.SetActive(value);
			}
		}

		[SerializeField]
		private float TargetFrames = 60;
		public int targetFps {
			get {
				return (int) TargetFrames;
			}
			set {
				TargetFrames = value;
				Application.targetFrameRate = value;
			}
		}

		// Start is called before the first frame update
		void Start() {
			instance = this;
			Application.targetFrameRate = targetFps;
			FpsCount.gameObject.SetActive(DisplayFps);
			timeLeft = timeInterval;
			timeLeftAv = timeIntervalAv;
		}

		private void OnGUI() {
			//if the count is active
			if (FpsCount.gameObject.activeInHierarchy) {
				//update all values
				timeLeft -= Time.unscaledDeltaTime;
				timeLeftAv -= Time.unscaledDeltaTime;
				accum += Time.unscaledDeltaTime;
				accumAv += Time.unscaledDeltaTime;
				frames++;
				framesAv++;

				//update av info if time up
				if (timeLeftAv <= 0.0f) {

					//calculate min frames
					prevCountAv = Mathf.RoundToInt((float)framesAv / accumAv);
					timeLeftAv = timeIntervalAv;
					accumAv = 0;
					framesAv = 0;
				}

				//update min info if time up
				if (timeLeft <= 0.0f) {

					//calculate min frames
					prevCount = Mathf.RoundToInt((float)frames / accum);
					timeLeft = timeInterval;
					accum = 0;
					frames = 0;
					//formate text
					string format = System.String.Format("FPS:{0}/{1}({2}%)", prevCountAv, prevCount, Mathf.RoundToInt(((float)prevCountAv / (float)targetFps) * 100.0f));

					//change color
					if (prevCountAv <= (targetFps / 6)) {
						FpsCount.color = Color.red;
					} else if (prevCountAv <= (targetFps / 2)) {
						FpsCount.color = Color.yellow;
					} else {
						FpsCount.color = Color.green;
					}

					FpsCount.text = format;
				}
			}
		}

	}
}