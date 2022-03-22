using Gameplay.Character.Leveling;
using Gameplay.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UI.GameUI;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Character
{
    public class HealthController : ITargetable
    {
        private float _maxHealth;
        private float _currentHealth;
        private CharactersStatusView _characterStatus;
        private GameObject _character;
        public bool isImmune = false;
        private int _xpFromDeath = 200;

        public void SetParams(float health, CharactersStatusView characterStatus, GameObject character)
        {
            _maxHealth = health;
            _currentHealth = _maxHealth;
            _character = character;
            _characterStatus = characterStatus;
        }

        public void ApplyDamage(int damage)
        {
            if (!isImmune)
            {
                _currentHealth -= damage;
                _characterStatus.CharacterTakeDamage(_currentHealth / _maxHealth);
                if (_currentHealth <= 0)
                {
                    Death();
                }
                Debug.Log(damage);
            }
        }
        private void Death()
        {
            UnityEngine.GameObject.Destroy(_character);
            Collider[] hitColliders = Physics.OverlapSphere(_character.transform.position, 5);
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
