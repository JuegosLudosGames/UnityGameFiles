using JLG.gift.cSharp.inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JLG.gift.cSharp.background {
	[System.Serializable]
	public class SerializableJLGDictionary {

		public int[] keys;
		public SerializableItemStack[] values;

		public SerializableJLGDictionary(Dictionary<int, SerializableItemStack> d) {
			keys = new int[d.Count];
			values = new SerializableItemStack[d.Count];

			int c = 0;
			foreach (int k in d.Keys) {
				keys[c] = k;
				values[c] = d[k];
				c++;
			}
		}

		public static implicit operator SerializableJLGDictionary(Dictionary<int, SerializableItemStack> d) {
			return new SerializableJLGDictionary(d);
		}

		public static implicit operator Dictionary<int, SerializableItemStack>(SerializableJLGDictionary d) {

			Dictionary<int, SerializableItemStack> r = new Dictionary<int, SerializableItemStack>();

			if (!(d.keys is null)) {
				if (d.keys.Length != 0) {
					for (int x = 0; x < d.keys.Length; x++) {
						r.Add(d.keys[x], d.values[x]);
					}
				}
			}

			return r;
		} 

	}
}