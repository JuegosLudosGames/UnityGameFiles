using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JLG.gift.cSharp.ui.overlay {
	[ExecuteAlways]
	public class GradualFader : MonoBehaviour {

		public Sprite sourceImage;
		public Color sourceColor = Color.white;
		public Sprite defaultImage;
		public Color defaultColor = Color.white;
		public Color emptyColor = Color.white;

		[Range(0.0f, 1.0f)]
		public float fill = 0;

		public Image fillI;
		public Image background;

		// Start is called before the first frame update
		void Start() {	
		
		}

		// Update is called once per frame
		void Update() {
			background.color = emptyColor;
			background.sprite = (sourceImage == null) ? defaultImage : sourceImage;
				
			fillI.color = (sourceImage == null) ? defaultColor : sourceColor;
			fillI.sprite = (sourceImage == null) ? defaultImage : sourceImage;
			fillI.fillAmount = fill;
		}
	}
}