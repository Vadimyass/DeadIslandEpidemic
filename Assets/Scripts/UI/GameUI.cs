using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DeadIsland.Events;
using Gameplay.Character.Ability.AbilityEvents;
using Gameplay.Character.Ability;
using Gameplay.Character;
using TMPro;
using Gameplay.Character.Ability.UpgradeEvents;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Image _firstAbilityImage;
    [SerializeField] private Image _secondAbilityImage;
    [SerializeField] private Image _thirdAbilityImage;
    [SerializeField] private Image _ultimateAbilityImage;

    [SerializeField] private Image _firstAbilityCooldownMeter;
    [SerializeField] private Image _secondAbilityCooldownMeter;
    [SerializeField] private Image _thirdAbilityCooldownMeter;
    [SerializeField] private Image _ultimateAbilityCooldownMeter;

    [SerializeField] private AbilityContainer _abilities;

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
    [SerializeField] private LevelController _levelController;
    void Start()
    {
        this.BindGameEventObserver<FirstAbilityEvent>((eventBase) => StartCooldown(_firstAbilityCooldownMeter, _abilities.firstAbility, _firstAbilityCooldown));
        this.BindGameEventObserver<SecondAbilityEvent>((eventBase) => StartCooldown(_secondAbilityCooldownMeter, _abilities.secondAbility, _secondAbilityCooldown));
        this.BindGameEventObserver<ThirdAbilityEvent>((eventBase) => StartCooldown(_thirdAbilityCooldownMeter, _abilities.thirdAbility, _thirdAbilityCooldown));
        this.BindGameEventObserver<UltimateAbilityEvent>((eventBase) => StartCooldown(_ultimateAbilityCooldownMeter, _abilities.ultimateAbility, _ultimateAbilityCooldown));
        this.BindGameEventObserver<UpLevelEvent>(UpPlayerLevel);
        this.BindGameEventObserver<FirstAbilityUpgradeEvent>((eventBase) => UpAbilityLevel(_firstAbilityLevel, _abilities.firstAbility));
        this.BindGameEventObserver<SecondAbilityUpgradeEvent>((eventBase) => UpAbilityLevel(_secondAbilityLevel, _abilities.secondAbility));
        this.BindGameEventObserver<ThirdAbilityUpgradeEvent>((eventBase) => UpAbilityLevel(_thirdAbilityLevel, _abilities.thirdAbility));
        this.BindGameEventObserver<UltimateAbilityUpgradeEvent>((eventBase) => UpAbilityLevel(_ultimateAbilityLevel, _abilities.ultimateAbility));
        CheckForUpgradingPosibility();
        _firstAbilityImage.sprite = _abilities.firstAbility.abilityImage;
        _secondAbilityImage.sprite = _abilities.secondAbility.abilityImage;
        _thirdAbilityImage.sprite = _abilities.thirdAbility.abilityImage;
        _ultimateAbilityImage.sprite = _abilities.ultimateAbility.abilityImage;
    }
    public virtual void StartCooldown(Image abilityCooldownMeter, Ability ability, TextMeshProUGUI abilityCooldown)
    {
        StartCoroutine(CooldownSkill(abilityCooldownMeter, ability, abilityCooldown));
    }
    private IEnumerator CooldownSkill(Image abilityCooldownMeter, Ability ability, TextMeshProUGUI abilityCooldown)
    {
        yield return new WaitForEndOfFrame();
        abilityCooldown.gameObject.SetActive(true);
        while (ability._onCooldown)
        {
            abilityCooldownMeter.fillAmount = ability.currentCooldown/ability._cooldown;
            abilityCooldown.text = ability.currentCooldown.ToString("0.0");
            yield return new WaitForSeconds(0.01f);
        }
        abilityCooldown.gameObject.SetActive(false);
    }
    private void UpPlayerLevel(EventBase eventBase)
    {
        new WaitForFixedUpdate();
        _level.text = (_levelController.level).ToString();
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
        if(ability.level == ability.maxLevel)
        {
            return false;
        }
        else if (_levelController.level >= ability.minLvlForUpgrade[ability.level] && _levelController.upgradePoints > 0)
        {
            return true;
        }
        return false;
    }

    public void UpAbilityLevel(Image abilityLevel, Ability ability)
    {
        new WaitForFixedUpdate();
        abilityLevel.fillAmount = (float)ability.level / (float)ability.maxLevel;
        CheckForUpgradingPosibility();
    }

    public void TriggerUpAbilityLevelEvent(int skillNumber)
    {
        Debug.Log("Clicked");
        if(skillNumber == 1)
        {
            new FirstAbilityUpgradeEvent().Invoke();
        }
        else if(skillNumber == 2)
        {
            new SecondAbilityUpgradeEvent().Invoke();
        }
        else if(skillNumber == 3)
        {
            new ThirdAbilityUpgradeEvent().Invoke();
        }
        else if(skillNumber == 4)
        {
            new UltimateAbilityUpgradeEvent().Invoke();
        }
    }
}
