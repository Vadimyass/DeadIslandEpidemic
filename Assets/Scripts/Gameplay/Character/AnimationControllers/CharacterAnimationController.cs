﻿using System;
using DeadIsland.Events;
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
            _animator.SetFloat("Speed",_characterMovement.SpeedMagnitude);
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