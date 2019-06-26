using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JLG.gift.cSharp.formating {
	[ExecuteInEditMode]
	public class TextComponentFormatter : MonoBehaviour {

		public bool isUI;
		public bool shouldRichFormat = true;

		public Text text_UI;
		public TextMesh text_gb;

		private bool changeLastFrame;

		[TextArea]
		public string text;
		private string back;

		private void Start() {
			updateText();
		}

		private void Update() {
			if (back != text) {
				updateText();
				back = text;
			}
		}

		// Update is called once per frame
		public void updateText(string text) {
			this.text = text;

			string nText;
			if (shouldRichFormat)
				nText = TextFormatter.translateCodes(text);
			else
				nText = TextFormatter.getRaw(text);

			if (isUI) {
				if (text_UI is null)
					return;
				text_UI.supportRichText = true;
				text_UI.text = nText;
			} else {
				if (text_gb is null)
					return;
				text_gb.richText = true;
				text_gb.text = nText;
			}
		}

		public void updateText() {
			string nText;
			if (shouldRichFormat)
				nText = TextFormatter.translateCodes(text);
			else
				nText = TextFormatter.getRaw(text);

			if (isUI) {
				if (text_UI is null)
					return;
				text_UI.supportRichText = true;
				text_UI.text = nText;
			} else {
				if (text_gb is null)
					return;
				text_gb.richText = true;
				text_gb.text = nText;
			}
		}

	}
}