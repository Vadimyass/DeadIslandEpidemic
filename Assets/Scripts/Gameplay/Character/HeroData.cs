using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gameplay.Character
{
    public class HeroData : MonoBehaviour
    {
        public string heroName;

        public float damageAmplification;
        public float attackSpeed;

        public string meleeWeaponName;
        public float meleeWeaponModificator;

        public string rangeWeaponName;
        public float rangeWeaponModificator;

        public string firstAbilityName;
        public string secondAbilityName;
        public string thirdAbilityName;
        public string ultimateAbilityName;

        public int strength;
        public int health;
        public int movementSpeed;

        public float meleeDamageAplification;
        public float rangeDamageAplification;
    }
}
