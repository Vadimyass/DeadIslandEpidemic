﻿using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Vector3 = UnityEngine.Vector3;

namespace Gameplay.Character.MovementControllers
{
    public enum CharacterMovementState
    {
        Idle,
        Movement,
        Attack
    }
    public class MovementController : MonoBehaviour
    {
        private float _speed = 7;

        public float SpeedMagnitude => _playerMovement.magnitude * _speed;
        
        private float rotateSpeedMovement = 0.075f;
        private float rotateVelocity;
        private int _rangeAttack = 1;
        private Vector3 _playerMovement;
        public CombatController combat;

        private CharacterMovementState _characterMovementState;
        public CharacterMovementState CharacterMovementState => _characterMovementState;

        private void Update()
        {
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                _playerMovement = new Vector3(horizontal, 0, vertical);

                if (horizontal == 0 && vertical == 0)
                {
                    _characterMovementState = CharacterMovementState.Idle;
                    return;
                }

                _playerMovement.Normalize();

                transform.Translate(_playerMovement * _speed * Time.deltaTime, Space.World);
                if (_playerMovement != Vector3.zero)
                {
                    transform.forward = _playerMovement;
                }

                _characterMovementState = CharacterMovementState.Movement;
        }

        public void SetMovementState(CharacterMovementState movementState)
        {
            _characterMovementState = movementState;
        }
        
        // Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
                            // float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                            //     rotationToLookAt.eulerAngles.y,
                            //     ref rotateVelocity,
                            //     rotateSpeedMovement * Time.deltaTime);
                            //
                            // transform.eulerAngles = new Vector3(0, rotationY, 0);
    }
}