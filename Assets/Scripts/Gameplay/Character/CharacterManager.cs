using System;
using Gameplay.CameraController;
using Gameplay.Character.Abilities;
using Gameplay.Character.AnimationControllers;
using Gameplay.Character.Leveling;
using Gameplay.Character.MovementControllers;
using Gameplay.Character.Weapons;
using System.Collections;
using System.Collections.Generic;
using UI.GameUI;
using UnityEngine;

namespace Gameplay.Character {
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] private CombatController _combatController;
        [SerializeField] private HeroLeveling _heroLeveling;
        [SerializeField] private MovementController _movement;
        [SerializeField] private CharacterAnimationController _characterAnimation;
        [SerializeField] private HeroData _heroData;
        [SerializeField] private Ability _firstAbility;
        [SerializeField] private Ability _secondAbility;
        [SerializeField] private Ability _thirdAbility;
        [SerializeField] private Ability _ultimateAbility;
        [SerializeField] private AbilityContainer _abilityContainer;
        private HealthController _health;
        [SerializeField] private MeleeWeapon _meleeWeapon;
        [SerializeField] private RangeWeapon _rangeWeapon;
        [SerializeField] private CharactersStatusView _characterStatus;
        [SerializeField] private PlayerStatusView _statusView;

        private void Awake()
        {
            _health = new HealthController();
            
            SetAllParameters(GoogleSheetLoader.Instance.GetTable());
        }

        private void SetAllParameters(HeroSheetsData data)
        {
            _heroData.SetData(data);
            _movement.SetParams(_heroData.movementSpeed,_characterAnimation);
            _movement.enabled = true;
            _combatController.SetParams(_meleeWeapon, _rangeWeapon, _characterAnimation, _heroData.attackSpeed);
            _abilityContainer.SetParams(_firstAbility, _secondAbility, _thirdAbility, _ultimateAbility);
            _heroLeveling.SetParams(_abilityContainer);
            _health.SetParams(_heroData.health, _characterStatus, gameObject);
            _statusView.SetParams(_heroLeveling, _abilityContainer);
        }
    } 
}
