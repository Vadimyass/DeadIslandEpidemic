using Gameplay.Character.Leveling;
using Gameplay.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UI.GameUI;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Character
{
    public enum CharacterSide
    {
        Survivor,
        Undead
    }
    public class HealthController : ITargetable
    {
        private float _maxHealth;
        private float _currentHealth;
        private CharactersStatusView _characterStatus;
        private GameObject _character;
        public bool isImmune = false;
        private int _xpFromDeath = 200;
        public CharacterSide characterSide;

        public void SetParams(float health, CharactersStatusView characterStatus, GameObject character, CharacterSide _characterSide)
        {
            characterSide = _characterSide;
            _maxHealth = health;
            _currentHealth = _maxHealth;
            _character = character;
            _characterStatus = characterStatus;
        }

        public void ApplyHeal(int heal)
        {
            _currentHealth += heal;
            _characterStatus.CharacterChangeHealth(_currentHealth / _maxHealth);
            Debug.Log(heal);
        }
        public void ApplyDamage(int damage)
        {
            if (!isImmune)
            {
                _currentHealth -= damage;
                _characterStatus.CharacterChangeHealth(_currentHealth / _maxHealth);
                if (_currentHealth <= 0)
                {
                    Death();
                }
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
