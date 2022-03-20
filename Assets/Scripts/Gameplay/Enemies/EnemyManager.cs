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

        private void Awake()
        {
            new HealthController().SetParams(_hp, _characterStatus, gameObject);
        }
    }
}
