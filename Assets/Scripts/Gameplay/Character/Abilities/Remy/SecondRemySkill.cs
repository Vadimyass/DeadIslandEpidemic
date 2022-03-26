using System;
using Gameplay.Interfaces;
using DeadIsland.Events;
using Gameplay.Character.Abilities.AbilityEvents;
using UnityEngine;
using Gameplay.Character.Abilities;

namespace Gameplay.Character.Abilities.Remy
{
    public class SecondRemySkill : Ability
    {
        private float _radius = 5;
        private void Awake()
        {
            this.BindGameEventObserver<SecondAbilityPressEvent>(OnPress);
            this.BindGameEventObserver<SecondAbilityEvent>(UseAbility);
        }
        public override void UpLevel()
        {
            base.UpLevel();
            damage *= 1.2f;
            if (level == 4)
            {
                _radius *= 1.5f;
            }
        }
        public override void TriggerAbilityEvent()
        {
            if (isPressed)
            {
                new SecondAbilityEvent().Invoke();
                base.TriggerAbilityEvent();
            }
        }
        public override void UseAbility()
        {
            if (!onCooldown && level != 0)
            {
                base.UseAbility();
                Collider[] hitColliders = Physics.OverlapSphere(this.gameObject.transform.position, _radius);
                foreach (Collider hitCollider in hitColliders)
                {
                    if (hitCollider.gameObject.TryGetComponent(out Health target))
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
