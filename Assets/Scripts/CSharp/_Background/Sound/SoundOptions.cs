using JLG.gift.cSharp.jglScripts.timeline;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace JLG.gift.cSharp.background.sound {
	[RequireComponent(typeof(AudioSource), typeof(PlayableDirector))]
	public class SoundOptions : ITimeControlInteract {

		public static SoundOptions instance;

		//options part

		[SerializeField]
		[Header("Options")]
		private List<AudioClip> AttackOptions;

		//public AudioListener listener;

		public AudioClip attack {get { return AttackOptions[Random.Range(0, AttackOptions.Capacity)]; }}
		public bool musicMute { get; private set; }
		public bool sfxMute { get; private set; }

		int masterVolume = 50;
		public int MasterVolume {
			get {
				return masterVolume;
			}
			set {
				masterVolume = value;
				AudioListener.volume = masterVolume / 100;
			}
		}

		int musicVolume = 50;
		public int MusicVolume {
			get {
				return musicVolume;
			}
			set {
				musicVolume = value;
				source.volume = musicVolume / 100;
			}
			
		}
		public int SfxVolume = 50;

		//music manager
		[SerializeField]
		[Header("MusicManager")]
		private AudioClip defaultMusic;
		public AudioClip DefaultMusic { get { return defaultMusic; } }
		public float fadeTime;


		private bool fadeIn = false;	//are we fading music in
		private bool fadeOut = false;   //are we fading music out
		private bool endTrackLoop = false;
		private AudioClip sendTo;		//clip that wil be sent to after fade

		private AudioSource source;     //source for background music
		private PlayableDirector director;

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

		public override PlayableDirector getDirector() {
			return director;
		}

		public override bool shouldContinueLoop() {
			if (!endTrackLoop) {
				return true;
			} else {
				endTrackLoop = false;
				return false;
			}
		}

		public enum SoundType {
			SFX, MUSIC
		}

	}
}