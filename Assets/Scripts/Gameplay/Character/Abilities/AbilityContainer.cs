using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Abilities
{
    public class AbilityContainer : MonoBehaviour
    {
        public Ability firstAbility;
        public Ability secondAbility;
        public Ability thirdAbility;
        public Ability ultimateAbility;

        public void SetParams(Ability _firstAbility, Ability _secondAbility, Ability _thirdAbility, Ability _ultimateAbility)
        {
            firstAbility = _firstAbility;
            secondAbility = _secondAbility;
            thirdAbility = _thirdAbility;
            ultimateAbility = _ultimateAbility;
        }
    }
}
