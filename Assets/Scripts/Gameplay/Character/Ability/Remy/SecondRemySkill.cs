using System;
using Gameplay.Interfaces;
using DeadIsland.Events;
using Gameplay.Character.Ability.AbilityEvents;
using UnityEngine;
using Gameplay.Character.Ability;

public class SecondRemySkill : Ability
{
    private float radius = 5;
    private void Awake()
    {
        this.BindGameEventObserver<SecondAbilityEvent>(OnPress);
    }
    public override void UpLevel()
    {
        base.UpLevel();
        damage *= 1.2f;
        if(level == 4)
        {
            radius *= 1.5f;
        }
    }
    public override void OnPress()
    {
        if (!_onCooldown && level != 0)
        {
            base.OnPress();
            Collider[] hitColliders = Physics.OverlapSphere(this.gameObject.transform.position, radius);
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
