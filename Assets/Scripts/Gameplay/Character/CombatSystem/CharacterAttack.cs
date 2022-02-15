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

        public int damage;
        public int AttackRange;

        public virtual void Init(AttackType attackType)
        {
            _attackType = attackType;

        }

        public virtual void SetAttackState()
        {
            Debug.LogError(_attackType.ToString());
            _characterAnimator.PlayAttackState(_attackType);
        }

        public virtual void Shoot()
        {
            _characterAnimator.PlayAttackAnimation();
        }
    }
}
