using JLG.gift.cSharp.background.scene.background;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.background.input {
	[System.Serializable]
	public class SerializableKeyInput {

		//asset name of the default binding
		private static readonly string DEFAULT_BINDING_NAME = "DefaultBinding";

		//json formatting for each keytype
		private static readonly string JUMP = "jump";
		private static readonly string LEFT = "left";
		private static readonly string RIGHT = "right";
		private static readonly string DASH = "dash";
		private static readonly string PHYSICAL = "physical";
		private static readonly string MAGIC1 = "magic1";
		private static readonly string MAGIC2 = "magic2";
		private static readonly string USE = "use";
		private static readonly string SWITCHLEFT = "switch_left";
		private static readonly string SWITCHRIGHT = "switch_right";
		private static readonly string INVENTORY = "inventory";

		//dictionary for each key binding
		public SerializableJLGStringIntDictionary KeyToKeyCode;

		//constructor for serializable
		public SerializableKeyInput(KeyInput key) {
			//inits dictionary
			Dictionary<string, int> keyToKeyCode = new Dictionary<string, int>();

			//gets the default keybinding for reference
			//KeyInput d = AssetBundle.FindObjectOfType<KeyInput>();
			//KeyInput d = Resources.Load<KeyInput>("customData/Keybindings (DO NOT MOVE)/DefaultBinding.asset");
			KeyInput d = GlobalItems.instance.defaultBinding;

			if (d is null)
				Debug.Log("Broken");
			if (key is null)
				Debug.Log("Broken 2");

			//adds all the keys to dictionary in which if key not found, default is used
			addKey(JUMP, key.Jump_Button, d.Jump_Button, keyToKeyCode);
			addKey(LEFT, key.Left_Button, d.Left_Button, keyToKeyCode);
			addKey(RIGHT, key.Right_Button, d.Right_Button, keyToKeyCode);
			addKey(DASH, key.Dash_Button, d.Dash_Button, keyToKeyCode);
			addKey(PHYSICAL, key.Physical_Button, d.Physical_Button, keyToKeyCode);
			addKey(MAGIC1, key.Magic1_Button, d.Magic1_Button, keyToKeyCode);
			addKey(MAGIC2, key.Magic2_Button, d.Magic2_Button, keyToKeyCode);
			addKey(USE, key.UseItem_Button, d.UseItem_Button, keyToKeyCode);
			addKey(SWITCHLEFT, key.SwitchItemLeft_Button, d.SwitchItemLeft_Button, keyToKeyCode);
			addKey(SWITCHRIGHT, key.SwitchItemRight_Button, d.SwitchItemRight_Button, keyToKeyCode);
			addKey(INVENTORY, key.InventoryToggle_Button, d.InventoryToggle_Button, keyToKeyCode);

		}

		//adds key to dictionary if val is not the default
		private void addKey(string key, KeyCode val, KeyCode def, Dictionary<string, int> d) {
			//checks if its the same as default
			if (val != def) {
				//if different, add to dictionary
				d.Add(key, (int) val);
			}
		}

		//tries to get a keycode, otherwise returns default
		private KeyCode tryToGet(string key, KeyCode def, Dictionary<string, int> d) {
			int t = 0;
			//if keycode is found
			if (d.TryGetValue(key, out t)) {
				//return value
				return (KeyCode)t;
			} else {
				//otherwise default
				return def;
			}
		}

		//casting for converting Keyinput into Serialzable
		public static implicit operator SerializableKeyInput(KeyInput key) {
			return new SerializableKeyInput(key);
		}

		//converts serializable into keyinput
		public static implicit operator KeyInput(SerializableKeyInput key) {

			//gets default
			//KeyInput d = Resources.Load<KeyInput>("customData/Keybindings (DO NOT MOVE)/DefaultBinding.asset");
			KeyInput d = GlobalItems.instance.defaultBinding;
			Dictionary<string, int> keyToKeyCode = key.KeyToKeyCode;

			//tries to store all the keycodes for each binding
			KeyCode jump = key.tryToGet(JUMP, d.Jump_Button, keyToKeyCode);
			KeyCode left = key.tryToGet(LEFT, d.Left_Button, keyToKeyCode);
			KeyCode right = key.tryToGet(RIGHT, d.Right_Button, keyToKeyCode);
			KeyCode dash = key.tryToGet(DASH, d.Dash_Button, keyToKeyCode);
			KeyCode phy = key.tryToGet(PHYSICAL, d.Physical_Button, keyToKeyCode);
			KeyCode magic1 = key.tryToGet(MAGIC1, d.Magic1_Button, keyToKeyCode);
			KeyCode magic2 = key.tryToGet(MAGIC2, d.Magic2_Button, keyToKeyCode);
			KeyCode use = key.tryToGet(USE, d.UseItem_Button, keyToKeyCode);
			KeyCode sleft = key.tryToGet(SWITCHLEFT, d.SwitchItemLeft_Button, keyToKeyCode);
			KeyCode sright = key.tryToGet(SWITCHRIGHT, d.SwitchItemRight_Button, keyToKeyCode);
			KeyCode inv = key.tryToGet(INVENTORY, d.InventoryToggle_Button, keyToKeyCode);

			//returns keyinput
			return new KeyInput(jump, left, right, dash, phy, magic1, magic2, use, sleft, sright, inv);
		}

	}
}