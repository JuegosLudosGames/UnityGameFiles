using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.ui.menuField {
	public interface MenuField<T> {
		T getValue();
		void setStartValue(T value);
	}
}