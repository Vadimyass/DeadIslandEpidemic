using Gameplay.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstRemySkill : Ability
{
    [SerializeField] private int _damage;
    [SerializeField] private MeleeWeapon _meleeWeapon;

    public override void OnPress()
    {
        if (!_onCooldown)
        {
            base.OnPress();
            Collider[] hitColliders = Physics.OverlapSphere(_meleeWeapon.attackPoint.transform.position, 2f);
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
