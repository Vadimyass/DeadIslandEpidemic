using System;
using DeadIsland.Events;
using Gameplay.Character.MovementControllers;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using static Gameplay.Character.CombatController;

namespace Gameplay.Character.AnimationControllers
{
    public class CharacterAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private bool _isAttacking = false;

        [Inject]
        private void Construct()
        {
            _animator = gameObject.GetComponent<Animator>();
        }

        
        public void SetSpeedToBlendTree(float speed)
        {
            _animator.SetFloat("Speed",speed);
        }

        public void PlayAttackState(AttackType attackType)
        {
            bool isMeleeAttack = attackType == AttackType.Melee;
            _animator.SetBool(AnimationNameType.isMelee.ToString(), isMeleeAttack);
            ClientSend.PlayerAnimationBool(AnimationNameType.isMelee,isMeleeAttack);
        }

        public void PlayAttackAnimation()
        {
            _animator.SetTrigger(AnimationNameType.Attack.ToString());
            ClientSend.PlayerAnimationTrigger(AnimationNameType.Attack);
            _isAttacking = true;
        }

        public void OnAttackFinished()
        {
            _isAttacking = false;
        }

        public void RefreshAttackSpeed(float attackSpeed)
        {
            float atkSpeed = attackSpeed / 100.0f;
            _animator.SetFloat("AttackSpeed", atkSpeed);
        }
        public void RefreshMovementSpeed(float movementSpeed)
        {
            float mvmSpeed = movementSpeed / 100.0f;
            //_characterMovement.movementSpeed = mvmSpeed;
            //_animator.SetFloat("MoveSpeed", mvmSpeed);
        }
    }
}