using Gameplay.Character.Ability;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Ability
{
    public class AbilityContainer : MonoBehaviour
    {
        [SerializeField] public Ability firstAbility;
        [SerializeField] public Ability secondAbility;
        [SerializeField] public Ability thirdAbility;
        [SerializeField] public Ability ultimateAbility;
    }
}
