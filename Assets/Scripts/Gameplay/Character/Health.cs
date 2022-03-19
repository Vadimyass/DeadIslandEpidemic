using Gameplay.Character.Leveling;
using Gameplay.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UI.GameUI;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Character
{
    public class Health : MonoBehaviour, ITargetable
    {
        [SerializeField] private float _maxHealth;
        private float _currentHealth;
        [SerializeField] private CharactersStatusView _characterStatus;
        public bool isImmune = false;
        private int _xpFromDeath = 200;
        private void Start()
        {
            _currentHealth = _maxHealth;
        }

        public void ApplyDamage(int damage)
        {
            if (!isImmune)
            {
                _currentHealth -= damage;
                _characterStatus.healthBar.fillAmount = _currentHealth / _maxHealth;
                if (_currentHealth <= 0)
                {
                    Death();
                }
                Debug.Log(damage);
            }
        }
        private void Death()
        {
            Destroy(gameObject);
            Collider[] hitColliders = Physics.OverlapSphere(this.gameObject.transform.position, 5);
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.TryGetComponent(out HeroLeveling player))
                {
                    player.TakeXP(_xpFromDeath);
                }
            }
        }
    }
}
