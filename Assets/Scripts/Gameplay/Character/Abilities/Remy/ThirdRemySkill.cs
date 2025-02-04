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
        public virtual IEnumerator OnPressed()
        {
            Vector3 targetpoint;
            Quaternion targetRotation;
            float hitdist;
            Ray ray;
            Plane playerplane;
            isPressed = true;
            skillOverview.SetActive(true);
            while (isPressed)
            {
                playerplane = new Plane(Vector3.up, transform.position);
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (playerplane.Raycast(ray, out hitdist))
                {
                    targetpoint = ray.GetPoint(hitdist);
                    targetRotation = Quaternion.LookRotation(targetpoint - transform.position);
                    startDrawPoint.transform.rotation = targetRotation;
                }
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
