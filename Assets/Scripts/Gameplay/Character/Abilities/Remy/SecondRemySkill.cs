using System;
using Gameplay.Interfaces;
using DeadIsland.Events;
using Gameplay.Character.Abilities.AbilityEvents;
using UnityEngine;
using Gameplay.Character.Abilities;
using System.Collections;

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
        public override void OnPress()
        {
            base.OnPress();
            if (!onCooldown && level != 0)
            {
                StartCoroutine(OnPressed());
            }
        }
        public virtual IEnumerator OnPressed()
        {
            isPressed = true;
            skillOverview.SetActive(true);
            while (isPressed)
            {
                yield return new WaitForEndOfFrame();
            }
            Collider[] hitColliders = Physics.OverlapSphere(this.gameObject.transform.position, 20000000000000000);
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.TryGetComponent(out Outline target))
                {
                    target.enabled = false;
                }
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
