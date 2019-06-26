using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.background.sound {
	[RequireComponent(typeof(AudioSource))]
	public class SoundOptions : MonoBehaviour {

		public static SoundOptions instance;

		//options part

		[SerializeField]
		[Header("Options")]
		private List<AudioClip> AttackOptions;


		public AudioClip attack {get { return AttackOptions[Random.Range(0, AttackOptions.Capacity)]; }}
		public bool musicMute { get; private set; }
		public bool sfxMute { get; private set; }

		//music manager
		[SerializeField]
		[Header("MusicManager")]
		private AudioClip defaultMusic;
		public AudioClip DefaultMusic { get { return defaultMusic; } }
		public float fadeTime;


		private bool fadeIn = false;	//are we fading music in
		private bool fadeOut = false;	//are we fading music out
		private AudioClip sendTo;		//clip that wil be sent to after fade

		private AudioSource source;		//source for background music

		private void Start() {
			//sets current instance 
			instance = this;
			//gets the audio source
			source = GetComponent<AudioSource>();
			//set it to looping
			source.loop = true;
			//sets clip to default
			source.clip = defaultMusic;
			//plays the clip
			source.Play();
		}

		private void Update() {
			//are we fading out
			if (fadeOut) {
				//fades music out slowly
				source.volume -= (1 / fadeTime) * Time.deltaTime;
				//is fading done?
				if (source.volume <= 0) {
					//stop fading
					fadeOut = false;
					//stop clip change clip and play it
					source.Stop();
					source.clip = sendTo;
					source.Play();
					//start fading in
					fadeIn = true;
				}
			//are we fading in
			} else if (fadeIn) {
				//fade music
				source.volume += (1 / fadeTime) * Time.deltaTime;
				//is fading done
				if (source.volume >= 1) {
					//stop fading
					fadeIn = false;
				}
			}

			//set mute status if music is muted
			source.mute = musicMute;

			//if (triggerChange) {
			//	triggerChange = false;
			//	setTrack(defaultMusic);
			//} else if (change) {
			//	change = false;
			//	setTrack(test);
			//}
		}

		//fades track for fading
		public void setTrack(AudioClip sendTo) {
			this.sendTo = sendTo;
			fadeOut = true;
			fadeIn = false;
		}

		//sets mute status to a sound type
		public void Mute(SoundType type, bool status) {
			if (type == SoundType.SFX) {
				sfxMute = status;
			} else if (type == SoundType.MUSIC) {
				musicMute = status;
			}
		}

		public enum SoundType {
			SFX, MUSIC
		}

	}
}