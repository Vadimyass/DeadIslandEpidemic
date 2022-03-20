using Gameplay.Character.AnimationControllers;
using System;
using UnityEngine;
using static Gameplay.Character.CombatController;

namespace Gameplay.Character.CombatSystem
{
    public abstract class CharacterAttack
    {
        public AttackType attackType;
        public CharacterAnimationController characterAnimator;
        public virtual void SetAttackState()
        {
            characterAnimator.PlayAttackState(attackType);
        }

        public virtual void Shoot()
        {
            characterAnimator?.PlayAttackAnimation();
        }
    }
}
