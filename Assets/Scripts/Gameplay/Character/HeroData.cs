using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gameplay.Character
{
    public class HeroData : MonoBehaviour
    {
        public int heroName;

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
            firstAbilityName = realData.IdFirstSkill.ToString();
            secondAbilityName = realData.IdSecondSkill.ToString();
            thirdAbilityName = realData.IdThirdSkill.ToString();
            ultimateAbilityName = realData.IdUltimateSkill.ToString();
        }
    }

}
