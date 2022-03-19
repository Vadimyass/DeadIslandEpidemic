using Gameplay.Character.AnimationControllers;
using System.Collections;
using System.Collections.Generic;
using DeadIsland.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Character.Abilities
{
    public class Ability : MonoBehaviour
    {
        [SerializeField] public Sprite abilityImage;
        [SerializeField] public float cooldown;
        public float currentCooldown;
        [SerializeField] public CharacterAnimationController animationController;

        [SerializeField] public float damage;
        public float damageMultiplier = 1.0f;
        public float realDamage => damage * damageMultiplier;

        private int name;
        private int description;
        public bool onCooldown;

        public int level;
        public int maxLevel;

        public int[] minLvlForUpgrade = new int[4];

        public virtual void UpLevel()
        {
            if (level < maxLevel)
            {
                
                level++;
            }
        }
        public virtual void OnPress(EventBase eventBase)
        {
            OnPress();
        }
        public virtual void OnPress()
        {
            StartCoroutine(OnCooldown());
        }
        private IEnumerator OnCooldown()
        {
            onCooldown = true;
            currentCooldown = cooldown;
            while (currentCooldown > 0)
            {
                currentCooldown -= 0.01f;
                yield return new WaitForSeconds(0.01f);
            }
            OnEndCooldown();
        }

        public virtual void OnEndCooldown()
        {
            onCooldown = false;
        }
        public void RotateCharacaterByTheMouse()
        {
            Plane playerplane = new Plane(Vector3.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitdist;

            if (playerplane.Raycast(ray, out hitdist))
            {
                Vector3 targetpoint = ray.GetPoint(hitdist);
                Quaternion targetrotation = Quaternion.LookRotation(targetpoint - transform.position);
                transform.rotation = targetrotation;
            }
        }
    }
}
