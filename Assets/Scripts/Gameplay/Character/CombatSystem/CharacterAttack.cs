using Gameplay.Character.AnimationControllers;
using System;
using UnityEngine;
using static Gameplay.Character.CombatController;

namespace Gameplay.Character.CombatSystem
{
    public abstract class CharacterAttack
    {
        public AttackType _attackType;
        public CharacterAnimationController _characterAnimator;
        public virtual void SetAttackState()
        {
            _characterAnimator.PlayAttackState(_attackType);
        }

        public virtual void Shoot()
        {
            _characterAnimator.PlayAttackAnimation();
        }
    }
}
