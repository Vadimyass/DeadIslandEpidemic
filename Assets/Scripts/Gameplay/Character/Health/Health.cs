using Gameplay.Character;
using Gameplay.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, ITargetable
{
    public HealthController healthController;
    public CharacterSide characterSide;
    private bool _isBandaging = false;

    public void SetParams(HealthController _healthController, CharacterSide _characterSide)
    {
        healthController = _healthController;
        characterSide = _characterSide;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (characterSide == CharacterSide.Survivor)
            {
                if (!_isBandaging)
                {
                    StartCoroutine(Bandaging());
                }
            }
        }

    }
    private IEnumerator Bandaging()
    {
        _isBandaging = true;
        int bandageTime = 10;
        int heal = 10;
        while(bandageTime > 0)
        {
            yield return new WaitForSeconds(1);
            ApplyHeal(heal);
            heal *= 2;
            bandageTime -= 1;
        }
        _isBandaging = false;
    }
    public void ApplyHeal(int heal)
    {
        healthController.ApplyHeal(heal);
    }
    public void ApplyDamage(int damage)
    {
        healthController.ApplyDamage(damage);
    }
}
