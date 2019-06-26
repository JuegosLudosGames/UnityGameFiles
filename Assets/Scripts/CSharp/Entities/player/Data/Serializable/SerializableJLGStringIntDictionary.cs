using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.background {
	[System.Serializable]
	public class SerializableJLGStringIntDictionary {
		public string[] keys;
		public int[] values;

		public SerializableJLGStringIntDictionary(Dictionary<string, int> d) {
			keys = new string[d.Count];
			values = new int[d.Count];

			int c = 0;
			foreach (string k in d.Keys) {
				keys[c] = k;
				values[c] = d[k];
				c++;
			}
		}

		public static implicit operator SerializableJLGStringIntDictionary(Dictionary<string, int> d) {
			return new SerializableJLGStringIntDictionary(d);
		}

		public static implicit operator Dictionary<string, int>(SerializableJLGStringIntDictionary d) {

			Dictionary<string, int> r = new Dictionary<string, int>();

			if (!(d.keys is null)) { 
				if (d.keys.Length != 0) {
					for (int x = 0; x < d.keys.Length; x++) {
						r.Add(d.keys[x], d.values[0]);
					}
				}
			}

			return r;
		}
	}
}