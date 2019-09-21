using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.background.input {
	[CreateAssetMenu(fileName = "new Keybinding", menuName = "Keybinding", order = 52)]
	public class KeyInput : ScriptableObject {

		//non-static
		[SerializeField]
		public KeyCode Jump_Button;
		[SerializeField]
		public KeyCode Left_Button;
		[SerializeField]
		public KeyCode Right_Button;
		[SerializeField]
		public KeyCode Dash_Button;
		[SerializeField]
		public KeyCode Physical_Button;
		[SerializeField]
		public KeyCode Magic1_Button;
		[SerializeField]
		public KeyCode Magic2_Button;
		[SerializeField]
		public KeyCode UseItem_Button;
		[SerializeField]
		public KeyCode SwitchItemLeft_Button;
		[SerializeField]
		public KeyCode SwitchItemRight_Button;
		[SerializeField]
		public KeyCode InventoryToggle_Button;

		public KeyInput(KeyCode jump, KeyCode left, KeyCode right, KeyCode da, KeyCode phy, KeyCode mag1, KeyCode mag2, KeyCode use, KeyCode switchL, KeyCode switchR, KeyCode inv) =>
			(Jump_Button, Left_Button, Right_Button, Dash_Button, Physical_Button, Magic1_Button, Magic2_Button, UseItem_Button, SwitchItemLeft_Button, SwitchItemRight_Button, InventoryToggle_Button) =
			(jump, left, right, da, phy, mag1, mag2, use, switchL, switchR, inv);

		//static
		public static KeyCode Jump;
		public static KeyCode Left;
		public static KeyCode Right;
		public static KeyCode Dash;
		public static KeyCode Physical;
		public static KeyCode Magic1;
		public static KeyCode Magic2;
		public static KeyCode UseItem;
		public static KeyCode SwitchItemLeft;
		public static KeyCode SwitchItemRight;
		public static KeyCode InventoryToggle;

		public static void LoadKeySet(KeyInput set) {
			(Jump, Left, Right, Dash, Physical, Magic1, Magic2, UseItem, SwitchItemLeft, SwitchItemRight, InventoryToggle) = (set.Jump_Button, set.Left_Button, set.Right_Button,
																										set.Dash_Button,
																										set.Physical_Button, set.Magic1_Button, set.Magic2_Button, 
																										set.UseItem_Button, set.SwitchItemLeft_Button,
																										set.SwitchItemRight_Button, set.InventoryToggle_Button);
		}

		public static KeyInput GetActiveKeySet() {
			KeyInput set = new KeyInput(Jump, Left, Right, Dash, Physical, Magic1, Magic2, UseItem, SwitchItemLeft, SwitchItemRight, InventoryToggle);
			return set;
		}

	}
}