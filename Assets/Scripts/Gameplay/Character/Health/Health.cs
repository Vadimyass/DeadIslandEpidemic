using Gameplay.Character;
using Gameplay.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, ITargetable
{
    private HealthController _healthController; 

    public void SetParams(HealthController healthController)
    {
        _healthController = healthController;
    }

    public void ApplyDamage(int damage)
    {
        _healthController.ApplyDamage(damage);
    }
}
