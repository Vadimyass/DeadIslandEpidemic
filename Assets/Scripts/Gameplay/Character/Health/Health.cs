using Gameplay.Character;
using Gameplay.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Health : MonoBehaviour, ITargetable
    {
        public HealthController healthController;
        public CharacterSide characterSide;


        public void SetParams(HealthController _healthController, CharacterSide _characterSide)
        {
            healthController = _healthController;
            characterSide = _characterSide;
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
