using System.Collections;
using System.Collections.Generic;
using Gameplay.Character.MovementControllers;
using Gameplay.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Gameplay.Character.Leveling;
using UI.GameUI;

namespace Gameplay.Enemies.Zombies 
{
    public class ZombieMelee : MonoBehaviour
    {
        [SerializeField] private int _damage;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ITargetable target))
            {
                target.ApplyDamage(_damage);
            }
        }
    }
}
