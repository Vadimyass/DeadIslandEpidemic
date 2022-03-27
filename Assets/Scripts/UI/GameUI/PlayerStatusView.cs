using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DeadIsland.Events;
using Gameplay.Character.Abilities.AbilityEvents;
using Gameplay.Character.Abilities;
using Gameplay.Character.Leveling;
using Gameplay.Character.Leveling.Events;
using TMPro;
using Gameplay.Character.Abilities.UpgradeEvents;
using Gameplay.Character;

namespace UI.GameUI
{
    public class PlayerStatusView : MonoBehaviour
    {
        private HeroLeveling _heroLeveling;
        private AbilitiesData _abilities;


        [SerializeField] private Image _firstAbilityImage;
        [SerializeField] private Image _secondAbilityImage;
        [SerializeField] private Image _thirdAbilityImage;
        [SerializeField] private Image _ultimateAbilityImage;

        [SerializeField] private Image _firstAbilityCooldownMeter;
        [SerializeField] private Image _secondAbilityCooldownMeter;
        [SerializeField] private Image _thirdAbilityCooldownMeter;
        [SerializeField] private Image _ultimateAbilityCooldownMeter;

        [SerializeField] private TextMeshProUGUI _firstAbilityCooldown;
        [SerializeField] private TextMeshProUGUI _secondAbilityCooldown;
        [SerializeField] private TextMeshProUGUI _thirdAbilityCooldown;
        [SerializeField] private TextMeshProUGUI _ultimateAbilityCooldown;

        [SerializeField] private Button _firstAbilityUpgrade;
        [SerializeField] private Button _secondAbilityUpgrade;
        [SerializeField] private Button _thirdAbilityUpgrade;
        [SerializeField] private Button _ultimateAbilityUpgrade;

        [SerializeField] private Image _firstAbilityLevel;
        [SerializeField] private Image _secondAbilityLevel;
        [SerializeField] private Image _thirdAbilityLevel;
        [SerializeField] private Image _ultimateAbilityLevel;

        [SerializeField] private Image _xpMeter;
        [SerializeField] private TextMeshProUGUI _level;

        [SerializeField] private Button _testButton;
        public void SetParams(HeroLeveling heroLeveling, AbilitiesData abilityContainer)
        {
            _abilities = abilityContainer;
            _heroLeveling = heroLeveling;
            this.BindGameEventObserver<UpLevelEvent>(UpPlayerLevel);

            this.BindGameEventObserver<FirstAbilityEvent>((eventBase) => StartCooldown(_firstAbilityCooldownMeter, _abilities.firstAbility, _firstAbilityCooldown));
            this.BindGameEventObserver<SecondAbilityEvent>((eventBase) => StartCooldown(_secondAbilityCooldownMeter, _abilities.secondAbility, _secondAbilityCooldown));
            this.BindGameEventObserver<ThirdAbilityEvent>((eventBase) => StartCooldown(_thirdAbilityCooldownMeter, _abilities.thirdAbility, _thirdAbilityCooldown));
            this.BindGameEventObserver<UltimateAbilityEvent>((eventBase) => StartCooldown(_ultimateAbilityCooldownMeter, _abilities.ultimateAbility, _ultimateAbilityCooldown));

            this.BindGameEventObserver<FirstAbilityUpgradeEvent>((eventBase) => UpAbilityLevel(_firstAbilityLevel, _abilities.firstAbility, _firstAbilityCooldownMeter));
            this.BindGameEventObserver<SecondAbilityUpgradeEvent>((eventBase) => UpAbilityLevel(_secondAbilityLevel, _abilities.secondAbility, _secondAbilityCooldownMeter));
            this.BindGameEventObserver<ThirdAbilityUpgradeEvent>((eventBase) => UpAbilityLevel(_thirdAbilityLevel, _abilities.thirdAbility, _thirdAbilityCooldownMeter));
            this.BindGameEventObserver<UltimateAbilityUpgradeEvent>((eventBase) => UpAbilityLevel(_ultimateAbilityLevel, _abilities.ultimateAbility, _ultimateAbilityCooldownMeter));

            CheckForUpgradingPosibility();

            _firstAbilityUpgrade.onClick.AddListener(() => { new FirstAbilityUpgradeEvent().Invoke(); });
            _secondAbilityUpgrade.onClick.AddListener(() => { new SecondAbilityUpgradeEvent().Invoke(); });
            _thirdAbilityUpgrade.onClick.AddListener(() => { new ThirdAbilityUpgradeEvent().Invoke(); });
            _ultimateAbilityUpgrade.onClick.AddListener(() => { new UltimateAbilityUpgradeEvent().Invoke(); });

            _testButton.onClick.AddListener(() => { _heroLeveling.TakeXP(200); });

            this.BindGameEventObserver<TakeXpEvent>(TakeXP);

            _firstAbilityImage.sprite = _abilities.firstAbility.abilityImage;
            _secondAbilityImage.sprite = _abilities.secondAbility.abilityImage;
            _thirdAbilityImage.sprite = _abilities.thirdAbility.abilityImage;
            _ultimateAbilityImage.sprite = _abilities.ultimateAbility.abilityImage;
        }
        public void StartCooldown(Image abilityCooldownMeter, Ability ability, TextMeshProUGUI abilityCooldown)
        {
            StartCoroutine(CooldownSkill(abilityCooldownMeter, ability, abilityCooldown));
        }
        private IEnumerator CooldownSkill(Image abilityCooldownMeter, Ability ability, TextMeshProUGUI abilityCooldown)
        {
            yield return new WaitForEndOfFrame();
            abilityCooldown.gameObject.SetActive(true);
            while (ability.onCooldown)
            {
                abilityCooldownMeter.fillAmount = ability.currentCooldown / ability.cooldown;
                abilityCooldown.text = ability.currentCooldown.ToString("0.0");
                yield return new WaitForSeconds(0.01f);
            }
            abilityCooldown.gameObject.SetActive(false);
        }
        private void UpPlayerLevel(EventBase eventBase)
        {
            new WaitForFixedUpdate();
            _level.text = (_heroLeveling.level).ToString();
            CheckForUpgradingPosibility();
        }

        private void CheckForUpgradingPosibility()
        {
            _firstAbilityUpgrade.gameObject.SetActive(CanBeUpgraded(_abilities.firstAbility));
            _secondAbilityUpgrade.gameObject.SetActive(CanBeUpgraded(_abilities.secondAbility));
            _thirdAbilityUpgrade.gameObject.SetActive(CanBeUpgraded(_abilities.thirdAbility));
            _ultimateAbilityUpgrade.gameObject.SetActive(CanBeUpgraded(_abilities.ultimateAbility));
        }
        private bool CanBeUpgraded(Ability ability)
        {
            if (ability.level == ability.maxLevel)
            {
                return false;
            }
            else if (_heroLeveling.level >= ability.minLvlForUpgrade[ability.level] && _heroLeveling.upgradePoints > 0)
            {
                return true;
            }
            return false;
        }

        public void UpAbilityLevel(Image abilityLevel, Ability ability, Image abilityCooldownMeter)
        {
            if (ability.level == 1)
            {
                abilityCooldownMeter.fillAmount = 0;
            }
            if (ability.level < ability.maxLevel)
            {
                new WaitForFixedUpdate();
                abilityLevel.fillAmount = (float)ability.level / (float)ability.maxLevel;
                CheckForUpgradingPosibility();
            }
        }

        private void TakeXP(EventBase eventBase)
        {
            new WaitForFixedUpdate();
            _xpMeter.fillAmount = (float)(_heroLeveling.xp - _heroLeveling.needXP[_heroLeveling.level - 2]) / (float)(_heroLeveling.needXP[_heroLeveling.level - 1] - _heroLeveling.needXP[_heroLeveling.level - 2]);
        }
    }
}
