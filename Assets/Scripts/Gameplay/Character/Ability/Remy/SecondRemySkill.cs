using System;
using Gameplay.Interfaces;
using DeadIsland.Events;
using Gameplay.Character.Ability.AbilityEvents;
using UnityEngine;

public class SecondRemySkill : Ability
{
    private void Awake()
    {
        this.BindGameEventObserver<SecondAbilityEvent>(OnPress);
    }

    public override void OnPress()
    {
        if (!_onCooldown)
        {
            base.OnPress();
            Collider[] hitColliders = Physics.OverlapSphere(this.gameObject.transform.position, 5);
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.TryGetComponent(out ITargetable target))
                {
                    target.ApplyDamage((int)realDamage);
                }
            }
        }
    }
}
