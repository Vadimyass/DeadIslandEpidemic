using Gameplay.Character.Abilities;
using Gameplay.Character.AnimationControllers;
using Gameplay.Character.MovementControllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character
{
    public class AbilitiesData
    {
        public Ability firstAbility;
        public Ability secondAbility;
        public Ability thirdAbility;
        public Ability ultimateAbility;

        public void SetData(HeroSkillsSheetsData data, HeroData heroData, CharacterAnimationController characterAnimationController, MovementController movementController, Ability firstAbility, Ability secondAbility, Ability thirdAbility, Ability ultimateAbility )
        {
            this.firstAbility = firstAbility;
            this.secondAbility = secondAbility;
            this.thirdAbility = thirdAbility;
            this.ultimateAbility = ultimateAbility;

            foreach(var skill in data.HeroSkillsList)
            {
                if(skill.Id == heroData.firstAbilityId)
                {
                    this.firstAbility.SetParams(skill, characterAnimationController, movementController);
                }
                if (skill.Id == heroData.secondAbilityId)
                {
                    this.secondAbility.SetParams(skill, characterAnimationController, movementController);
                }
                if (skill.Id == heroData.thirdAbilityId)
                {
                    this.thirdAbility.SetParams(skill, characterAnimationController, movementController);
                }
                if (skill.Id == heroData.ultimateAbilityId)
                {
                    this.ultimateAbility.SetParams(skill, characterAnimationController, movementController);
                }
            }
        }
    }
}
