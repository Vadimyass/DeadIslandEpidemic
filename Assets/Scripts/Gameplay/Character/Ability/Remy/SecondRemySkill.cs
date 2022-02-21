using Gameplay.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondRemySkill : Ability
{
    [SerializeField] private int _damage;

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
                    target.ApplyDamage(_damage);
                }
            }
        }
    }
}
