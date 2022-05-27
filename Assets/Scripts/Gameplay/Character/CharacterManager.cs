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
using Zenject;

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
        [SerializeField] private Health _health;
        private HealthController _healthController;
        [SerializeField] private MeleeWeapon _meleeWeapon;
        [SerializeField] private RangeWeapon _rangeWeapon;
        [SerializeField] private CharactersStatusView _characterStatus;
        [SerializeField] private PlayerStatusView _statusView;
        [SerializeField] private AbilitiesData _abilitiesData;
        [SerializeField] private Bandage _bandage;

        [Inject]
        private void Construct()
        {
            _healthController = new HealthController();
        }

        private void Start()
        {
            
            _abilitiesData = new AbilitiesData();

            SetAllParameters(GoogleSheetLoader.Instance.GetTable(), GoogleSheetLoader.Instance.GetSkillTable());
        }

        private void SetAllParameters(HeroSheetsData heroesData, HeroSkillsSheetsData heroSkillsData)
        {
            _heroData.SetData(heroesData);
            _abilitiesData.SetData(heroSkillsData, _heroData, _characterAnimation, _movement, _firstAbility, _secondAbility, _thirdAbility, _ultimateAbility);

            _movement.SetParams(_heroData.movementSpeed,_characterAnimation);
            _movement.enabled = true;
            _combatController.SetParams(_meleeWeapon, _rangeWeapon, _characterAnimation, _heroData.attackSpeed, _movement);
            _heroLeveling.SetParams(_abilitiesData);
            _healthController.SetParams(_heroData.health, _characterStatus, gameObject, CharacterSide.Survivor);
            _health.SetParams(_healthController, CharacterSide.Survivor);
            _statusView.SetParams(_heroLeveling, _abilitiesData);
            _bandage.SetParams(_health);
            Debug.Log("All params Seted");
        }
    } 
}
