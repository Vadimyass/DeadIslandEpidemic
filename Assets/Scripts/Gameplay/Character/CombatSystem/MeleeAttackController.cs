using Gameplay.Character.AnimationControllers;
using Gameplay.Character.Weapons;
using Gameplay.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Gameplay.Character.CombatController;

namespace Gameplay.Character.CombatSystem
{
    class MeleeAttackController: CharacterAttack
    {
        private MeleeWeapon _meleeWeapon;
        public MeleeAttackController(CharacterAnimationController animationController, Weapon meleeWeapon)
        {
            this.characterAnimator = animationController;
            attackType = AttackType.Melee;
            _meleeWeapon = meleeWeapon as MeleeWeapon;
        }
        
        public void DealDamage()
        {
            Collider[] hitColliders = Physics.OverlapSphere(_meleeWeapon.attackPoint.transform.position, 0.5f);
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.TryGetComponent(out ITargetable target))
                {
                    target.ApplyDamage((int)_meleeWeapon.realDamage);
                    break;
                }
            }
        }
    }
}
