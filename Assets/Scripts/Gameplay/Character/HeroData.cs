using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Gameplay.Character
{
    public class HeroData : MonoBehaviour
    {
        public string heroName;

        public float damageAmplification;
        public float attackSpeed;

        public string meleeWeaponName;
        public float meleeDamageAplification;

        public string rangeWeaponName;
        public float rangeDamageAplification;

        public int firstAbilityId;
        public int secondAbilityId;
        public int thirdAbilityId;
        public int ultimateAbilityId;

        public int power;
        public int health;
        public int movementSpeed;

        
        

        [Inject]
        private void Construct(CharacterConfig characterConfig)
        {
            heroName = characterConfig.HeroProperties[0];
            damageAmplification = float.Parse(characterConfig.HeroProperties[1]);
            attackSpeed = float.Parse(characterConfig.HeroProperties[2]);
            meleeWeaponName = characterConfig.HeroProperties[3];
            meleeDamageAplification = float.Parse(characterConfig.HeroProperties[4]);
            rangeWeaponName = characterConfig.HeroProperties[5];
            rangeDamageAplification = float.Parse(characterConfig.HeroProperties[6]);
            firstAbilityId = int.Parse(characterConfig.HeroProperties[7]);
            secondAbilityId = int.Parse(characterConfig.HeroProperties[8]);
            thirdAbilityId = int.Parse(characterConfig.HeroProperties[9]);
            ultimateAbilityId = int.Parse(characterConfig.HeroProperties[10]);
            power = int.Parse(characterConfig.HeroProperties[11]);
            health = int.Parse(characterConfig.HeroProperties[12]);
            movementSpeed = int.Parse(characterConfig.HeroProperties[13]);

        }
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
