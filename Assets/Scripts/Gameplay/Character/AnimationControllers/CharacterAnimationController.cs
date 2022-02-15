using System;
using Gameplay.Character.MovementControllers;
using UnityEngine;
using UnityEngine.AI;
using static Gameplay.Character.CombatController;

namespace Gameplay.Character.AnimationControllers
{
    public class CharacterAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private MovementController _characterMovement;
        private bool _isAttacking = false;

        private void Update()
        {
            if (_characterMovement.CharacterMovementState == CharacterMovementState.Movement)
            {
                _animator.SetFloat("Speed",_characterMovement.SpeedMagnitude);
            }
            else if(_characterMovement.CharacterMovementState == CharacterMovementState.Idle && !_isAttacking)
            {
                _animator.SetFloat("Speed",0);
            }
        }

        public void PlayAttackState(AttackType attackType)
        {
            bool isMeleeAttack = attackType == AttackType.Melee;
            _animator.SetBool("IsMelee", isMeleeAttack);
        }

        public void PlayAttackAnimation()
        {
            _animator.SetTrigger("Attack");
            _isAttacking = true;
        }

        public void OnAttackFinished()
        {
            Debug.LogError("finished");
            _isAttacking = false;
        }
    }
}