using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SheetProcessor : MonoBehaviour
{

    // a1,b1,c1,d1
    // a2,b2,c2,d2
    // a3,b3,c3,d3

    private const int _name = 0;
    private const int _power = 1;
    private const int _damageAmp = 2;
    private const int _health = 3;
    private const int _movementSpeed = 4;
    private const int _meleeDamageAmp = 5;
    private const int _rangeDamageAmp = 6;
    private const int _attackSpeed = 7;
    private const int _idFirstSkill = 8;
    private const int _idSecondSkill = 9;
    private const int _idThirdSkill = 10;
    private const int _idUltimateSkill = 11;

    private const char _cellSeporator = ',';
    private const char _inCellSeporator = ';';

    public HeroSheetsData ProcessData(string cvsRawData)
    {
        char lineEnding = GetPlatformSpecificLineEnd();
        string[] rows = cvsRawData.Split(lineEnding);
        int dataStartRawIndex = 1;
        HeroSheetsData data = new HeroSheetsData();
        data.HeroOptionsList = new List<HeroOptions>();
        for (int i = dataStartRawIndex; i < rows.Length; i++)
        {
            string[] cells = rows[i].Split(_cellSeporator);

            int name = ParseInt(cells[_name]);
            int power = ParseInt(cells[_power]);
            float damageAmp = ParseFloat(cells[_damageAmp]);
            int health = ParseInt(cells[_health]);
            int movementSpeed = ParseInt(cells[_movementSpeed]);
            float meleeDamageAmp = ParseFloat(cells[_meleeDamageAmp]);
            float rangeDamageAmp = ParseFloat(cells[_rangeDamageAmp]);
            int attackSpeed = ParseInt(cells[_attackSpeed]);
            int idFirstSkill = ParseInt(cells[_idFirstSkill]);
            int idSecondSkill = ParseInt(cells[_idSecondSkill]);
            int idThirdSkill = ParseInt(cells[_idThirdSkill]);   
            int idUltimateSkill = ParseInt(cells[_idUltimateSkill]);


            data.HeroOptionsList.Add(new HeroOptions()
            {
                Name = name,
                Power = power,
                DamageAmp = damageAmp,
                Health = health,
                MovementSpeed = movementSpeed,
                MeleeDamageAmp = meleeDamageAmp,
                RangeDamageAmp = rangeDamageAmp,
                AttackSpeed = attackSpeed,
                IdFirstSkill = idFirstSkill,
                IdSecondSkill = idSecondSkill,
                IdThirdSkill = idThirdSkill,
                IdUltimateSkill = idUltimateSkill
            }) ;
        }
        Debug.Log(data.HeroOptionsList.ToString());
        return data;

    }

    private Vector3 ParseVector3(string s)
    {
        string[] vectorComponents = s.Split(_inCellSeporator);
        if (vectorComponents.Length < 3)
        {
            Debug.Log("Can't parse Vector3. Wrong text format");
            return default;
        }

        float x = ParseFloat(vectorComponents[0]);
        float y = ParseFloat(vectorComponents[1]);
        float z = ParseFloat(vectorComponents[2]);
        return new Vector3(x, y, z);
    }

    private int ParseInt(string s)
    {
        int result = -1;
        if (!int.TryParse(s, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out result))
        {
            Debug.Log("Can't parse int, wrong text");
        }

        return result;
    }

    private float ParseFloat(string s)
    {
        float result = -1;
        if (!float.TryParse(s, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out result))
        {
            Debug.Log("Can't pars float,wrong text ");
        }

        return result;
    }

    private char GetPlatformSpecificLineEnd()
    {
        char lineEnding = '\n';
        return lineEnding;
    }
}