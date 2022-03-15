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
        _level.text = (_levelController.level+1).ToString();
        _firstAbilityUpgrade.gameObject.SetActive(true);
        _secondAbilityUpgrade.gameObject.SetActive(true);
        _thirdAbilityUpgrade.gameObject.SetActive(true);
        _ultimateAbilityUpgrade.gameObject.SetActive(true);
    }

    private void UpAbilityLevel(Image abilityLevel, Ability ability)
    {
        abilityLevel.fillAmount = (float)ability.level / (float)ability.maxLevel;
        if(_levelController.upgradePoints == 0)
        {
            _firstAbilityUpgrade.gameObject.SetActive(false);
            _secondAbilityUpgrade.gameObject.SetActive(false);
            _thirdAbilityUpgrade.gameObject.SetActive(false);
            _ultimateAbilityUpgrade.gameObject.SetActive(false);
        }
    }

    public void TriggerUpAbilityLevelEvent(int skillNumber)
    {
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
