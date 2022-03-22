using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character
{
    public class SkillsData : MonoBehaviour
    {
        public int firstSkillId;
        public string firstSkillName;
        public float firstSkillDamage;
        public float firstSkillHeal;
        public float firstSkillCooldown;

        public int secondSkillId;
        public string secondSkillName;
        public float secondSkillDamage;
        public float secondSkillHeal;
        public float secondSkillCooldown;

        public int thirdSkillId;
        public string thirdSkillName;
        public float thirdSkillDamage;
        public float thirdSkillHeal;
        public float thirdSkillCooldown;

        public int ultimateSkillId;
        public string ultimateSkillName;
        public float ultimateSkillDamage;
        public float ultimateSkillHeal;
        public float ultimateSkillCooldown;

        public void SetData(HeroSkillsSheetsData data, HeroData heroData)
        {
            foreach(var skill in data.HeroSkillsList)
            {
                if(skill.Id == heroData.firstAbilityId)
                {
                    firstSkillId = skill.Id;
                    firstSkillName = skill.Name;
                    firstSkillDamage = skill.Damage;
                    firstSkillHeal = skill.Heal;
                    firstSkillCooldown = skill.Cooldown;
                }
                if (skill.Id == heroData.secondAbilityId)
                {
                    secondSkillId = skill.Id;
                    secondSkillName = skill.Name;
                    secondSkillDamage = skill.Damage;
                    secondSkillHeal = skill.Heal;
                    secondSkillCooldown = skill.Cooldown;
                }
                if (skill.Id == heroData.thirdAbilityId)
                {
                    thirdSkillId = skill.Id;
                    thirdSkillName = skill.Name;
                    thirdSkillDamage = skill.Damage;
                    thirdSkillHeal = skill.Heal;
                    thirdSkillCooldown = skill.Cooldown;
                }
                if (skill.Id == heroData.ultimateAbilityId)
                {
                    ultimateSkillId = skill.Id;
                    ultimateSkillName = skill.Name;
                    ultimateSkillDamage = skill.Damage;
                    ultimateSkillHeal = skill.Heal;
                    ultimateSkillCooldown = skill.Cooldown;
                }
            }
        }
    }
}
