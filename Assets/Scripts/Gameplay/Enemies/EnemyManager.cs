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
        [SerializeField] private CharactersStatusView _characterStatus;
        [SerializeField] private Health _health;
        private HealthController _healthController;

        private void Awake()
        {
            _healthController = new HealthController();
            _healthController.SetParams(_hp, _characterStatus, gameObject);
            _health.SetParams(_healthController);
        }
    }
}
