using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using JLG.gift.cSharp.inventory;
using JLG.gift.cSharp.background.input;
using JLG.gift.cSharp.buildData;

namespace JLG.gift.cSharp.entity.player.data {
	public class PlayerData : MonoBehaviour {

		public static PlayerData instance;

		//public List<InvItem> defaultI;

		public Inventory inventory;

		public KeyInput keybinding;

		public BuildData buildData;

		public float strengthInc = 0;
		public float spellInc = 0;

		public float maxHealthAdd = 20.0f;
		public float maxManaAdd = 200.0f;

		private bool isinit = false;

		// Start is called before the first frame update
		void Start() {
			instance = this;
			//DontDestroyOnLoad(this);

			//InvItem[] a = defaultI.ToArray();

			////List<ItemStack> i = new List<ItemStack>();
			//ItemStack[] i2 = new ItemStack[a.Length];

			//for (int x = 0; x < a.Length; x++) {
			//	i2[x] = !(a[x] is null) ? new ItemStack(a[x], 1) : null;
			//}

			//List<ItemStack> i = i2.OfType<ItemStack>().ToList();
			//Debug.Log(defaultI.Capacity);
			//Debug.Log(defaultI.ToArray().Length);

			//for (int x = 0; x < defaultI.ToArray().Length; x++) {
			//	if (!(defaultI[x] is null)) {
			//		i.Insert(x, new ItemStack(defaultI[x], 1));
			//	} else {
			//		i.Insert(x, null);
			//	}

			//}

			//setup();

			//Debug.Log(i.Capacity);

			//inventory = new Inventory(new List<ItemStack>(), new List<ItemStack>(), new List<ItemStack>(), 55);
		}

		private void Update() {
			if(isinit)
				inventory.updateInv();
		}

		public IEnumerable startLoad() {
			SaveData sd = SaveData.loadedSave;
			inventory = sd.Inventory;
			yield return null;
			keybinding = sd.Keybinding;
			yield return null;
			KeyInput.LoadKeySet(keybinding);
			yield return null;
			buildData = BuildData.getDataFromId(sd.buildId);
			yield return null;
			strengthInc = sd.StrengthLevelIncrease;
			spellInc = sd.SpellStrengthLevelIncrease;
			maxHealthAdd = sd.MaxHealth;
			maxManaAdd = sd.MaxMana;
			isinit = true;
		}

		public void startNew() {
			inventory = new Inventory(new List<ItemStack>(), new List<ItemStack>(), new List<ItemStack>(), 55);
			KeyInput.LoadKeySet(keybinding);
			isinit = true;
		}

		//public void setup() {
		//	//maxXp = 
		//	KeyInput.LoadKeySet(keybinding);
		//}

		public void consume(ConsumableItem item) {
			maxHealthAdd += item.PermHealthIncrease;
			maxManaAdd += item.PermManaIncrease;
			strengthInc += item.PermStrengthIncrease;
			spellInc += item.PermSpellIncrease;
		}

	}
}