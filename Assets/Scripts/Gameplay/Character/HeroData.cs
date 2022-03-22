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

        public int firstAbilityId;
        public int secondAbilityId;
        public int thirdAbilityId;
        public int ultimateAbilityId;

        public int power;
        public int health;
        public int movementSpeed;

        public float meleeDamageAplification;
        public float rangeDamageAplification;

        public void SetData(HeroSheetsData data)
        {
            HeroOptions realData = data.HeroOptionsList[0];
            heroName = realData.Name;
            power = realData.Power;
            damageAmplification = realData.DamageAmp;
            health = realData.Health;
            movementSpeed = realData.MovementSpeed;
            meleeDamageAplification = realData.MeleeDamageAmp;
            rangeDamageAplification = realData.RangeDamageAmp;
            attackSpeed = realData.AttackSpeed;
            firstAbilityId = realData.IdFirstSkill;
            secondAbilityId = realData.IdSecondSkill;
            thirdAbilityId = realData.IdThirdSkill;
            ultimateAbilityId = realData.IdUltimateSkill;
        }
    }

}
