using System;
using Gameplay.Interfaces;
using System.Collections;
using System.Collections.Generic;
using DeadIsland.Events;
using Gameplay.Character.Abilities.AbilityEvents;
using UnityEngine;
using Gameplay.Character.Abilities;

namespace Gameplay.Character.Abilities.Remy
{
    public class ThirdRemySkill : Ability
    {
        [Range(0, 360)]
        [SerializeField] private float _angle;

        private void Awake()
        {
            this.BindGameEventObserver<ThirdAbilityEvent>(OnPress);
        }
        public override void UpLevel()
        {
            base.UpLevel();
            damage *= 1.2f;
            if (level == 4)
            {
                _angle += 20;
            }
        }
        public override void OnPress()
        {
            if (!onCooldown && level != 0)
            {
                base.OnPress();
                movementController.RotateCharacaterByTheMouse();
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5);
                foreach (Collider hitCollider in hitColliders)
                {
                    if (hitCollider.gameObject.TryGetComponent(out ITargetable target))
                    {
                        Vector3 dirToTarget = (hitCollider.transform.position - transform.position).normalized;
                        if (Vector3.Angle(transform.forward, dirToTarget) < _angle / 2)
                        {
                            target.ApplyDamage((int)realDamage);
                        }
                    }
                }
            }
        }
    }
}
