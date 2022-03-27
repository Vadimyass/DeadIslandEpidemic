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
            this.BindGameEventObserver<ThirdAbilityPressEvent>(OnPress);
            this.BindGameEventObserver<ThirdAbilityEvent>(UseAbility);
        }
        public override void OnPress()
        {
            base.OnPress();
            if (!onCooldown && level != 0)
            {
                StartCoroutine(OnPressed());
            }
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
        public override void TriggerAbilityEvent()
        {
            if (isPressed)
            {
                new ThirdAbilityEvent().Invoke();
                base.TriggerAbilityEvent();
            }
        }
        public override void UseAbility()
        {
            if (!onCooldown && level != 0)
            {
                base.UseAbility();
                movementController.RotateCharacaterByTheMouse();
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5);
                foreach (Collider hitCollider in hitColliders)
                {
                    if (hitCollider.gameObject.TryGetComponent(out Health target))
                    {
                        Vector3 dirToTarget = (hitCollider.transform.position - transform.position).normalized;
                        if (Vector3.Angle(transform.forward, dirToTarget) < _angle / 2)
                        {
                            if (target.characterSide == CharacterSide.Undead)
                            {
                                target.ApplyDamage((int)realDamage);
                            }
                        }
                    }
                }
            }
        }
    }
}
