using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeroSheetsData
{
    public List<HeroOptions> HeroOptionsList;

    public override string ToString()
    {
        string result = "";
        HeroOptionsList.ForEach(o =>
        {
            result += o.ToString();
        });
        return result;
    }
}
[System.Serializable]
public class HeroOptions
{
    public int Name;
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
