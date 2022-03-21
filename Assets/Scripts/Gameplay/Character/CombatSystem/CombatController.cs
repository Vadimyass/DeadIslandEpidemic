using System;
using System.Threading.Tasks;
using DeadIsland.Events;
using Gameplay.Character.Abilities.AbilityEvents;
using Gameplay.Character.AnimationControllers;
using Gameplay.Character.CombatSystem;
using Gameplay.Character.MovementControllers;
using Gameplay.Character.Weapons;
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
        private MeleeWeapon _meleeWeapon;
        private RangeWeapon _rangeWeapon;
        private CharacterAnimationController _animationController;

        private MovementController _movementController;
        private float _attackSpeed;

        private RangeAttackController _rangeAttackController;
        private MeleeAttackController _meleeAttackController;

        private CharacterAttack _currentAttackController;
        
        private AttackType _combatState;

        public void SetParams(MeleeWeapon meleeWeapon, RangeWeapon rangeWeapon, CharacterAnimationController animationController, float attackSpeed, MovementController movementController)
        {
            _meleeWeapon = meleeWeapon;
            _rangeWeapon = rangeWeapon;
            _animationController = animationController;
            _movementController = movementController;
            _attackSpeed = attackSpeed;
            
            Init(AttackType.Melee);
            _animationController.RefreshAttackSpeed(_attackSpeed);
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
                _movementController.RotateCharacaterByTheMouse();
                _currentAttackController.Shoot();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                new FirstAbilityEvent().Invoke();
            }
            if(Input.GetKeyDown(KeyCode.E))
            {
                new SecondAbilityEvent().Invoke();
            }
            if(Input.GetKeyDown(KeyCode.R))
            {
                new ThirdAbilityEvent().Invoke();
            }
            if(Input.GetKeyDown(KeyCode.F))
            {
                new UltimateAbilityEvent().Invoke();
            }
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
        public void RefreshDamage(float damageMultiplier)
        {
            /*_firstAbility.damageMultiplier = damageMultiplier;
            _secondAbility.damageMultiplier = damageMultiplier;
            _thirdAbility.damageMultiplier = damageMultiplier;
            _ultimateAbility.damageMultiplier = damageMultiplier;*/
            _meleeWeapon.damageMultiplier = damageMultiplier;
            _rangeWeapon.damageMultiplier = damageMultiplier;
        }
    }
}