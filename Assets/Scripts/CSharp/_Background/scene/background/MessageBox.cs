using JLG.gift.cSharp.formating;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JLG.gift.cSharp.background.scene.background {
	public class MessageBox : MonoBehaviour {

		public static MessageBox instance;

		public GameObject container;
		public float animateTime = 1.0f;
		public float bufferSpace = 0.5f;
		public float defaultTime = 3.0f;
		public float restXPost = 0.0f;

		public Color criticalColor = Color.red;
		public Color warningColor = Color.yellow;
		public Color infoColor;

		private Queue<MessageRequest> toPut;
		private float distanceOut = 0.0f;
		private float timeLeft = 0.0f;
		private bool isAnimating = false;
		private bool completed = false;
		private bool working = false;
		private float disPerSec = 0.0f;
		private float translateErrorBypass = 0.0f;

		// Start is called before the first frame update
		void Start() {
			container.SetActive(false);
			instance = this;
			toPut = new Queue<MessageRequest>();
		}

		// Update is called once per frame
		void Update() {
			if (working) {
				if (isAnimating && !completed) {
					RectTransform rt = container.transform as RectTransform;
					//get distance to move
					container.transform.Translate(Vector3.left * disPerSec * Time.deltaTime);

					if (Vector3.Distance(container.transform.localPosition, new Vector3(restXPost, 0, 0)) <= translateErrorBypass || container.transform.localPosition.x < restXPost) {
						isAnimating = false;
					}
				} else if (isAnimating && completed) {
					RectTransform rt = container.transform as RectTransform;
					container.transform.Translate(Vector3.right * disPerSec * Time.deltaTime);

					if (Vector3.Distance(container.transform.localPosition, new Vector3(distanceOut, 0, 0)) <= translateErrorBypass || container.transform.localPosition.x > distanceOut) {
						if (toPut.Count > 0) {
							MessageRequest a = toPut.Dequeue();
							startMessage(a.message, a.time, a.color);
						} else {
							working = false;
							container.SetActive(false);
						}
					}
				} else if (!isAnimating && !completed) {
					timeLeft -= Time.deltaTime;
					if (timeLeft <= 0) {
						isAnimating = true;
						completed = true;
					}
				}
			}
		}

		private void startMessage(string message, float time, Color color) {

			timeLeft = time;
			isAnimating = true;
			completed = false;
			working = true;

			container.SetActive(true);
			container.GetComponentInChildren<Text>().text = TextFormatter.translateCodes(message);
			container.GetComponent<Image>().color = color;


			Canvas.ForceUpdateCanvases();

			RectTransform rt = container.transform as RectTransform;

			distanceOut = rt.rect.width + bufferSpace;
			
			container.transform.localPosition = new Vector3(distanceOut, 0, 0);

			disPerSec = distanceOut / animateTime;
			translateErrorBypass = 2 / 53 * (disPerSec * disPerSec);

			if (translateErrorBypass < 2)
				translateErrorBypass = 2;
		}

		public void messageUser(string message, float time, Color color) {
			if (message is null || message == "")
				return;

			if (toPut.Count == 0 && !working) {
				startMessage(message, time, color);
			} else {
				MessageRequest a = new MessageRequest();
				a.message = message;
				a.time = time;
				a.color = color;
				toPut.Enqueue(a);
			}
		}

		public void messageInfo(string message, float time) {
			messageUser(message, time, infoColor);
		}

		public void messageInfo(string message) {
			messageUser(message, defaultTime, infoColor);
		}

		public void messageWarning(string message, float time) {
			messageUser(message, time, warningColor);
		}

		public void messageWarning(string message) {
			messageUser(message, defaultTime, warningColor);
		}

		public void messageCritical(string message, float time) {
			messageUser(message, time, criticalColor);
		}

		public void messageCritical(string message) {
			messageUser(message, defaultTime, criticalColor);
		}

		private struct MessageRequest {
			public string message;
			public float time;
			public Color color;
		}

	}
}