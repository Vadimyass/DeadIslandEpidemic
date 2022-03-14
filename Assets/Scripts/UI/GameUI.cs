using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DeadIsland.Events;
using Gameplay.Character.Ability.AbilityEvents;
using Gameplay.Character.Ability;

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

    [SerializeField] private FirstRemySkill _firstAbility;
    [SerializeField] private SecondRemySkill _secondAbility;
    [SerializeField] private ThirdRemySkill _thirdAbility;
    [SerializeField] private UltimateRemySkill _ultimateAbility;
    void Start()
    {
        this.BindGameEventObserver<FirstAbilityEvent>((eventBase) => StartCooldown(_firstAbilityCooldownMeter, _firstAbility));
        this.BindGameEventObserver<SecondAbilityEvent>((eventBase) => StartCooldown(_secondAbilityCooldownMeter, _secondAbility));
        this.BindGameEventObserver<ThirdAbilityEvent>((eventBase) => StartCooldown(_thirdAbilityCooldownMeter, _thirdAbility));
        this.BindGameEventObserver<UltimateAbilityEvent>((eventBase) => StartCooldown(_ultimateAbilityCooldownMeter, _ultimateAbility));
        _firstAbilityImage.sprite = _firstAbility.abilityImage;
        _secondAbilityImage.sprite = _secondAbility.abilityImage;
        _thirdAbilityImage.sprite = _thirdAbility.abilityImage;
        _ultimateAbilityImage.sprite = _ultimateAbility.abilityImage;
    }
    public virtual void StartCooldown(Image abilityCooldownMeter, Ability ability)
    {
        StartCoroutine(CooldownSkill(abilityCooldownMeter, ability));
    }
    private IEnumerator CooldownSkill(Image abilityCooldownMeter, Ability ability)
    {
        yield return new WaitForEndOfFrame();
        while (ability._onCooldown)
        {
            abilityCooldownMeter.fillAmount = ability.currentCooldown/ability._cooldown;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
