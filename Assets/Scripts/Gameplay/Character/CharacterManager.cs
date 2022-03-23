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
        [SerializeField] private Health _health;
        private HealthController _healthController;
        [SerializeField] private MeleeWeapon _meleeWeapon;
        [SerializeField] private RangeWeapon _rangeWeapon;
        [SerializeField] private CharactersStatusView _characterStatus;
        [SerializeField] private PlayerStatusView _statusView;
        [SerializeField] private SkillsData _skillsManager;

        private void Awake()
        {
            _healthController = new HealthController();
            
            SetAllParameters(GoogleSheetLoader.Instance.GetTable(), GoogleSheetLoader.Instance.GetSkillTable());
        }

        private void SetAllParameters(HeroSheetsData heroesData, HeroSkillsSheetsData heroSkillsData)
        {
            _heroData.SetData(heroesData);
            _skillsManager.SetData(heroSkillsData, _heroData);

            _movement.SetParams(_heroData.movementSpeed,_characterAnimation);
            _movement.enabled = true;
            _combatController.SetParams(_meleeWeapon, _rangeWeapon, _characterAnimation, _heroData.attackSpeed, _movement);

            _firstAbility.SetParams(_skillsManager.firstSkillId, _skillsManager.firstSkillCooldown, _characterAnimation, _movement, _skillsManager.firstSkillDamage, _skillsManager.firstSkillName,  _skillsManager.firstSkillHeal);        
            _secondAbility.SetParams(_skillsManager.secondSkillId, _skillsManager.secondSkillCooldown, _characterAnimation, _movement, _skillsManager.secondSkillDamage, _skillsManager.secondSkillName, _skillsManager.secondSkillHeal);
            _thirdAbility.SetParams(_skillsManager.thirdSkillId, _skillsManager.thirdSkillCooldown, _characterAnimation, _movement, _skillsManager.thirdSkillDamage, _skillsManager.thirdSkillName, _skillsManager.thirdSkillHeal);
            _ultimateAbility.SetParams(_skillsManager.ultimateSkillId, _skillsManager.ultimateSkillCooldown, _characterAnimation, _movement, _skillsManager.ultimateSkillDamage, _skillsManager.ultimateSkillName, _skillsManager.ultimateSkillHeal);

            _abilityContainer.SetParams(_firstAbility, _secondAbility, _thirdAbility, _ultimateAbility);
            _heroLeveling.SetParams(_abilityContainer);
            _healthController.SetParams(_heroData.health, _characterStatus, gameObject, CharacterSide.Survivor);
            _health.SetParams(_healthController, CharacterSide.Survivor);
            _statusView.SetParams(_heroLeveling, _abilityContainer);
            Debug.Log("All params Seted");
        }
    } 
}
