using System;
using Gameplay.Character.MovementControllers;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Character.AnimationControllers
{
    public class CharacterAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private MovementController _characterMovement;
        [SerializeField] private CombatController _characterCombat;
        private bool _isAttacking = false;

        private void Update()
        {
            if(_characterCombat.CombatState == CombatState.Melee)
            {
                _animator.SetBool("IsMelee", true);
            }
            else
            {
                _animator.SetBool("IsMelee", false);
            }
            if (_characterMovement.CharacterMovementState == CharacterMovementState.Movement)
            {
                _animator.SetFloat("Speed",_characterMovement.SpeedMagnitude);
            }
            else if(_characterMovement.CharacterMovementState == CharacterMovementState.Idle && !_isAttacking)
            {
                _animator.SetFloat("Speed",0);
            }
        }

        public void SetAttackState()
        {
            _isAttacking = true;
            _animator.SetTrigger("Attack");
        }

        private void OnAttackFinished()
        {
            _isAttacking = false;
        }
    }
}