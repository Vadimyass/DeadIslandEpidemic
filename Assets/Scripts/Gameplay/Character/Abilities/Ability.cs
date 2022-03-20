using Gameplay.Character.AnimationControllers;
using System.Collections;
using System.Collections.Generic;
using DeadIsland.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Character.Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        [SerializeField] public Sprite abilityImage;
        [SerializeField] public float cooldown;
        public float currentCooldown;
        [SerializeField] public CharacterAnimationController animationController;
        [SerializeField] public CombatController combatController;

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
            Debug.Log("Cooldown End");
            onCooldown = false;
        }
    }
}
