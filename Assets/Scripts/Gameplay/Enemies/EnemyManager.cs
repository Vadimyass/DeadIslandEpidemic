using Gameplay.Character;
using System.Collections;
using System.Collections.Generic;
using UI.GameUI;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private float _hp;
        [SerializeField] public CharactersStatusView characterStatus;
        [SerializeField] private Health _health;
        private HealthController _healthController;

        private void Awake()
        {
            _healthController = new HealthController();
            _healthController.SetParams(_hp, characterStatus, gameObject, CharacterSide.Undead);
            _health.SetParams(_healthController, CharacterSide.Undead);
        }
    }
}
