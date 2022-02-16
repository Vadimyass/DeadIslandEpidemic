using System;
using System.Threading.Tasks;
using Gameplay.Character.AnimationControllers;
using Gameplay.Character.CombatSystem;
using Gameplay.Character.MovementControllers;
using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay.Character
{
    
    public class CombatController : MonoBehaviour
    {
        public enum AttackType
        {
            Melee,
            Range
        }
        [SerializeField] private MeleeWeapon _meleeWeapon;
        [SerializeField] private RangeWeapon _rangeWeapon;
        [SerializeField] private CharacterAnimationController _animationController;
        private RangeAttackController _rangeAttackController;
        private MeleeAttackController _meleeAttackController;

        private CharacterAttack _currentAttackController;

        private float rotateSpeedMovement = 0.075f;
        private float rotateVelocity;
        float rotationY;
        
        private AttackType _combatState;

        void Start()
        {
            Init(AttackType.Melee);
        }

        public void Init(AttackType attackType)
        {
            _rangeAttackController = new RangeAttackController(_animationController, _rangeWeapon);
            _meleeAttackController = new MeleeAttackController(_animationController, _meleeWeapon);

            _combatState = attackType;

            SetCharacterAttackByAttackType();

            _currentAttackController.SetAttackState();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                _combatState = _combatState == AttackType.Melee ? AttackType.Range : AttackType.Melee;
                SetCharacterAttackByAttackType();
                _currentAttackController.SetAttackState();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                RotateCharacaterByTheMouse();
                _currentAttackController.Shoot();
            }
        }

        private void RotateCharacaterByTheMouse()
        {
            Quaternion rotationToLookAt = Quaternion.LookRotation(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                rotationToLookAt.eulerAngles.y,
                ref rotateVelocity,
                rotateSpeedMovement * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, rotationY, 0);
        }

        private void SetCharacterAttackByAttackType()
        {
            switch (_combatState)
            {
                case AttackType.Melee:
                    _currentAttackController = _meleeAttackController;
                    _rangeWeapon.gameObject.SetActive(false);
                    _meleeWeapon.gameObject.SetActive(true);
                    break;
                case AttackType.Range:
                    _currentAttackController =  _rangeAttackController;
                    _rangeWeapon.gameObject.SetActive(true);
                    _meleeWeapon.gameObject.SetActive(false);
                    break;
            }
        }


        public void DealDamage()
        {
            ((MeleeAttackController)_currentAttackController).DealDamage();
        }
    }
}