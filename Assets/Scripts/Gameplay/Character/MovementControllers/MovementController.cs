using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

namespace Gameplay.Character.MovementControllers
{
    public class MovementController : MonoBehaviour
    {
        private float _speed;
        public float movementSpeed = 1.0f;
        public float SpeedMagnitude => _playerMovement.magnitude;
        
        private Vector3 _playerMovement;
        


        public void SetParams(float moveSpeed)
        {
            _speed = moveSpeed;
        }
        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            
            _playerMovement = new Vector3(horizontal, 0, vertical);
            
            _playerMovement.Normalize();
            transform.Translate(_playerMovement * _speed * Time.deltaTime, Space.World);
            ClientSend.PlayerMovement(transform.position, _playerMovement);
            transform.forward =  _playerMovement != Vector3.zero ? _playerMovement : transform.forward;
        }
    }
}