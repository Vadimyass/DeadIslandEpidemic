using System;
using Gameplay.Character.AnimationControllers;
using Gameplay.Character.MovementControllers;
using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay.Character
{
    
    public class CombatController : MonoBehaviour
    {
        public enum CombatState
        {
            Melee,
            Range
        }
        public float AttackRange = 10;
        [SerializeField] private MovementController _movementController;
        [SerializeField] private CharacterAnimationController _animationController;
        [SerializeField] private Shotgun _shotgun;
        private float rotateSpeedMovement = 0.075f;
        private float rotateVelocity;
        float rotationY;
        private ITargetable _target;
        private CombatState _combatState;
        public CombatState сombatState => _combatState;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (_combatState == CombatState.Melee)
                {
                    _combatState = CombatState.Range;
                }
                else
                {
                    _combatState = CombatState.Melee;
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Rotate();
                RaycastHit hit;
                if (_combatState == CombatState.Melee)
                {
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, AttackRange))
                    {
                        if (hit.collider.TryGetComponent(out ITargetable target))
                        {
                            _target = target;
                        }
                    }
                }
                else
                {
                    Shoot();
                }
            }
        }

        private void Rotate()
        {
            Quaternion rotationToLookAt = Quaternion.LookRotation(Input.mousePosition - transform.position);
            rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                rotationToLookAt.eulerAngles.y,
                ref rotateVelocity,
                rotateSpeedMovement * Time.deltaTime);
            _animationController.SetAttackState();
            transform.eulerAngles = new Vector3(0, rotationY, 0);
        }
        private void Attack()
        {
            if (_target != null)
            {
                _target.ApplyDamage(10);
            }
        }
        private void Shoot()
        {
            Debug.Log("Babah");
            _shotgun.Shoot(transform);
        }
    }
}