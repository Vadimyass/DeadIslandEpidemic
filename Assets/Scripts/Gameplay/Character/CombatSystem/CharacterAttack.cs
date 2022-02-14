using Gameplay.Character.AnimationControllers;
using UnityEngine;
using static Gameplay.Character.CombatController;

namespace Gameplay.Character.CombatSystem
{
    public abstract class CharacterAttack
    {
        public AttackType _attackType;

        private CharacterAnimationController _characterAnimator;

        public int damage;
        public int AttackRange;

        public virtual void Init(AttackType attackType,CharacterAnimationController characterAnimationController)
        {
            _attackType = attackType;

            _characterAnimator = characterAnimationController;

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
