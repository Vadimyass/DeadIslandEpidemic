using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class HeroSheetsData
{
    public List<HeroOptions> HeroOptionsList;

    public override string ToString()
    {
        string result = "";
        HeroOptionsList.ForEach(hero =>
        {
            result += hero.ToString();
        });
        return result;
    }
}
[System.Serializable]
public class HeroOptions
{
    public string Name;
    public int Power;
    public float DamageAmp;
    public int Health;
    public int MovementSpeed;
    public float MeleeDamageAmp;
    public float RangeDamageAmp;
    public int AttackSpeed;
    public int IdFirstSkill;
    public int IdSecondSkill;
    public int IdThirdSkill;
    public int IdUltimateSkill;

    public override string ToString()
    {
        return $"Name {Name} \nPower {Power} \nDamageAmp {DamageAmp} \nHealth {Health} \nMovementSpeed {MovementSpeed} \nMeleeDamageAmp {MeleeDamageAmp} \nRangeDamageAmp {RangeDamageAmp} \nAttackSpeed {AttackSpeed} \nIdFirstSkill {IdFirstSkill} \nIdSecondSkill {IdSecondSkill} \nIdThirdSkill {IdThirdSkill} \nIdUltimateSkill {IdUltimateSkill}";
    }
}

[System.Serializable]
public class HeroSkillsSheetsData
{
    public List<HeroSkill> HeroSkillsList;

    public override string ToString()
    {
        string result = "";
        HeroSkillsList.ForEach(skill =>
        {
            result += skill.ToString();
        });
        return result;
    }
}

[System.Serializable]
public class HeroSkill
{
    public int Id;
    public string Name;
    public float Damage;
    public float Heal;
    public float Cooldown;
    public override string ToString()
    {
        return $"ID {Id} \nName {Name} \nDamage {Damage} \n Heal {Heal} \n Cooldown{Cooldown}";
    }
}