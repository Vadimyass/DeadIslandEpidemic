using Gameplay.Character.AnimationControllers;
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
        private ITargetable _target;
        private MeleeWeapon _meleeWeapon;

        public MeleeAttackController(CharacterAnimationController animationController, MeleeWeapon meleeWeapon)
        {
            this._characterAnimator = animationController;
            _attackType = AttackType.Melee;
            _meleeWeapon = meleeWeapon;
        }

        public override void Shoot()
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, _meleeWeapon.attackRange))
                {
                    if (hit.collider.TryGetComponent(out ITargetable target))
                    {
                        _target = target;
                    }
                }

            base.Shoot();
        }

        public void DealDamage()
        {
            _target?.ApplyDamage(_meleeWeapon.damage);
        }
    }
}
