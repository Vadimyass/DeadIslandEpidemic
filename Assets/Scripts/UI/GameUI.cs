using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private Ability _firstAbility;
    [SerializeField] private Ability _secondAbility;
    [SerializeField] private Ability _thirdAbility;
    [SerializeField] private Ability _ultimateAbility;
    void Start()
    {
        _firstAbilityImage.sprite = _firstAbility.abilityImage;
        _secondAbilityImage.sprite = _secondAbility.abilityImage;
        _thirdAbilityImage.sprite = _thirdAbility.abilityImage;
        _ultimateAbilityImage.sprite = _ultimateAbility.abilityImage;
    }

    void Update()
    {
        
    }
}
