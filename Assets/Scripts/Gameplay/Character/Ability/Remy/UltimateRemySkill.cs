using System;
using Gameplay.Character;
using System.Collections;
using System.Collections.Generic;
using DeadIsland.Events;
using Gameplay.Character.Ability.AbilityEvents;
using UnityEngine;

public class UltimateRemySkill : Ability
{
    [SerializeField] private CombatController _combatController;

    private void Awake()
    {
        this.BindGameEventObserver<UltimateAbilityEvent>(OnPress);
    }

    public override void OnPress()
    {
        if (!_onCooldown)
        {
            base.OnPress();
            StartCoroutine(Buff());
        }
    }

    private IEnumerator Buff()
    {
        animationController.RefreshMovementSpeed(125);
        animationController.RefreshAttackSpeed(130);
        _combatController.RefreshDamage(1.25f);
        Debug.Log("Remy is Raged");
        yield return new WaitForSeconds(8.0f);
        animationController.RefreshAttackSpeed(100);
        animationController.RefreshMovementSpeed(100);
        _combatController.RefreshDamage(1.0f);
        Debug.Log("Remy stop Rage");
    }
}
