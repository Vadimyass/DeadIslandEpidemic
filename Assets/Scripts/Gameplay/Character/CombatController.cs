using System;
using Gameplay.Character.AnimationControllers;
using Gameplay.Character.MovementControllers;
using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay.Character
{
    public class CombatController : MonoBehaviour
    {
        public float AttackRange = 10;
        [SerializeField] private MovementController _movementController;
        [SerializeField] private CharacterAnimationController _animationController;
        
        private float rotateSpeedMovement = 0.075f;
        private float rotateVelocity;
        float rotationY;
        private ITargetable _target;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, AttackRange))
                {
                    Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
                    rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                        rotationToLookAt.eulerAngles.y,
                        ref rotateVelocity,
                        rotateSpeedMovement * Time.deltaTime);
                    _animationController.SetAttackState();
                    transform.eulerAngles = new Vector3(0, rotationY, 0);
                    if (hit.collider.TryGetComponent(out ITargetable target))
                    {
                        _target = target;
                    }
                }
            }
        }

        private void Attack()
        {
            _target.ApplyDamage(10);
        }
    }
}