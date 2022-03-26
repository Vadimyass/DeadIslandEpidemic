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
        public bool onCooldown = false;

        public bool isPressed = false;

        public int level;
        public int maxLevel;

        public int[] minLvlForUpgrade = new int[4];

        public GameObject skillOverview;

        [SerializeField] public GameObject startDrawPoint;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPressed = false;
                skillOverview.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                skillOverview.SetActive(false);
                TriggerAbilityEvent();
            }
        }
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
        public virtual void TriggerAbilityEvent()
        {
            isPressed = false;
        }
        public virtual void UseAbility(EventBase eventBase)
        {
            UseAbility();
        }
        public virtual void OnPress(EventBase eventBase)
        {
            OnPress();
        }
        public virtual void UseAbility()
        {
            StartCoroutine(OnCooldown());
        }
        public virtual void OnPress()
        {
            if (!onCooldown && level != 0)
            {
                StartCoroutine(OnPressed());
            }
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
        public IEnumerator OnPressed()
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
            
        }
        public virtual void OnEndCooldown()
        {
            Debug.Log("Cooldown End");
            onCooldown = false;
        }
    }
}
