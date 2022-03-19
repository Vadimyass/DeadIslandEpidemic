using System;
using Gameplay.Character;
using System.Collections;
using System.Collections.Generic;
using DeadIsland.Events;
using Gameplay.Character.Ability.AbilityEvents;
using UnityEngine;
using Gameplay.Character.Ability;

namespace Gameplay.Character.Ability.Remy
{
    public class UltimateRemySkill : Ability
    {
        [SerializeField] private CombatController _combatController;

        private void Awake()
        {
            this.BindGameEventObserver<UltimateAbilityEvent>(OnPress);
        }
        public override void UpLevel()
        {
            base.UpLevel();
            damage *= 1.2f;
            if (level == 3)
            {
                cooldown -= 20;
            }
        }
        public override void OnPress()
        {
            if (!onCooldown && level != 0)
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
            yield return new WaitForSeconds(8.0f);
            animationController.RefreshAttackSpeed(100);
            animationController.RefreshMovementSpeed(100);
            _combatController.RefreshDamage(1.0f);
        }
    }
}
