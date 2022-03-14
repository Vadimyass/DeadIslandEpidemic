using System;
using System.Threading.Tasks;
using DeadIsland.Events;
using Gameplay.Character.Ability.AbilityEvents;
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

        [SerializeField] private float _attackSpeed;

        private RangeAttackController _rangeAttackController;
        private MeleeAttackController _meleeAttackController;

        private CharacterAttack _currentAttackController;
        
        private AttackType _combatState;

        void Start()
        {
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
                RotateCharacaterByTheMouse();
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

        private void RotateCharacaterByTheMouse()
        {
            Plane playerplane = new Plane(Vector3.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitdist;

            if (playerplane.Raycast(ray, out hitdist))
            {
                Vector3 targetpoint = ray.GetPoint(hitdist);
                Quaternion targetrotation = Quaternion.LookRotation(targetpoint - transform.position);
                if (_combatState == AttackType.Melee)
                {
                    transform.rotation = targetrotation;
                }
                else
                {
                    transform.rotation = targetrotation * Quaternion.Euler(0, 35, 0);
                }
       
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