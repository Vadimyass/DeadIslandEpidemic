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

        private ITargetable _target;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, AttackRange))
                {
                    if (hit.collider.TryGetComponent(out ITargetable target))
                    {
                        _animationController.SetAttackState();
                        _target = target;
                        Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
                        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                            rotationToLookAt.eulerAngles.y,
                            ref rotateVelocity,
                            rotateSpeedMovement * Time.deltaTime);

                        transform.eulerAngles = new Vector3(0, rotationY + 46f, 0);
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