using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.ui.menuField {
	public class FieldHolder : MonoBehaviour {

		public GameObject[] fields;

		public MenuField<T> getAtIndex<T>(int index) {
			return fields[index].GetComponent<MenuField<T>>();
		}

	}
}