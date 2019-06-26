using JLG.gift.cSharp.attackData.spells.spelltype;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLG.gift.cSharp.entity.ai.aistate {
	public class StateStraightMagic : AiState {

		//spell for state
		public Spell spell {
			get; private set;
		}
		//local id of spell
		public int num {
			get; private set;
		}

		//constructor
		public StateStraightMagic(Spell spell, int num) =>
			(this.spell, this.num) = (spell, num);
	}
}