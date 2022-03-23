using System.Collections;
using System.Collections.Generic;
using Gameplay.Character.MovementControllers;
using Gameplay.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Gameplay.Character.Leveling;
using UI.GameUI;
using Gameplay.Character;

namespace Gameplay.Enemies.Zombies 
{
    public class ZombieMelee : MonoBehaviour
    {
        [SerializeField] private int _damage;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Health target))
            {
                if (target.characterSide == CharacterSide.Survivor)
                {
                    target.ApplyDamage(_damage);
                }
            }
        }
    }
}
