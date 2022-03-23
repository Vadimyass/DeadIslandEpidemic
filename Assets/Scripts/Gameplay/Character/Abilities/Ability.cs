using Gameplay.Character.AnimationControllers;
using System.Collections;
using System.Collections.Generic;
using DeadIsland.Events;
using UnityEngine;
using UnityEngine.UI;
using Gameplay.Character.MovementControllers;

namespace Gameplay.Character.Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        private int _id;
        public Sprite abilityImage;
        public float cooldown;
        public float currentCooldown;
        public CharacterAnimationController animationController;
        public MovementController movementController;
        public float damage;
        public float heal;

        public float damageMultiplier = 1.0f;
        public float realDamage => damage * damageMultiplier;

        private string _name;
        private int description;
        public bool onCooldown;

        public int level;
        public int maxLevel;

        public int[] minLvlForUpgrade = new int[4];


        public void SetParams(HeroSkill skillData, CharacterAnimationController _characterAnimationController, MovementController _movementController)
        {
            _id = skillData.Id;
            _name = skillData.Name;
            heal = skillData.Heal;
            animationController = _characterAnimationController;
            movementController = _movementController;
            damage = skillData.Damage;
            cooldown = skillData.Cooldown;
            abilityImage = Resources.Load<Sprite>("AbilityImages/" + _id.ToString());
        }
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
