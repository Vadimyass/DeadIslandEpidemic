using System.Collections;
using System.Collections.Generic;
using DeadIsland.Events;
using Gameplay.Character.Abilities;
using Gameplay.Character.Abilities.AbilityEvents;
using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay.Character.Abilities.Remy
{
    public class FirstRemySkill : Ability
    {
        [SerializeField] private float _chargeSpeed;
        [SerializeField] private float _maximumDistance;
        private Health _hp;
        private Vector3 originPosition;
        private float _originalDamage;
        public override void UpLevel()
        {
            base.UpLevel();
            _originalDamage *= 1.2f;
            cooldown -= 1;
            if (level == 4)
            {
                _chargeSpeed *= 2;
            }
        }
        public void Start()
        {
            _originalDamage = damage;
            _hp = gameObject.GetComponent<Health>();
            this.BindGameEventObserver<FirstAbilityPressEvent>(OnPress);
            this.BindGameEventObserver<FirstAbilityEvent>(UseAbility);
        }
        public override void OnPress()
        {
            base.OnPress();
            if (!onCooldown && level != 0)
            {
                StartCoroutine(OnPressed());
            }
        }
        public override void TriggerAbilityEvent()
        {
            if (isPressed)
            {
                new FirstAbilityEvent().Invoke();
                base.TriggerAbilityEvent();
            }
        }
        public override void UseAbility()
        {
            if (!onCooldown && level != 0)
            {
                ClientSend.SendInvokeFirstSkill(Vector3.one);
                base.UseAbility();
                originPosition = transform.position;
                movementController.RotateCharacaterByTheMouse();
                StartCoroutine(Charge());
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
        private IEnumerator Charge()
        {
            _hp.healthController.isImmune = true;
            float damageFromDistance;
            while (Vector3.Distance(transform.position, originPosition) < _maximumDistance)
            {
                damageFromDistance = 1 + (Vector3.Distance(transform.position, originPosition) / _maximumDistance);
                damage = _originalDamage * damageFromDistance;
                transform.position += transform.forward * _chargeSpeed * 0.01f * (1 / (damageFromDistance));
                yield return new WaitForSeconds(0.01f);
            }
            _hp.healthController.isImmune = false;
            damage = _originalDamage;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (_hp.healthController.isImmune)
            {
                if (other.TryGetComponent(out Health target))
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
