using System;
using System.Numerics;
using Gameplay.Character.AnimationControllers;
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

        private CharacterAnimationController _characterAnimationController;
        
        private Vector3 _playerMovement;
        


        public void SetParams(float moveSpeed ,CharacterAnimationController characterAnimationController )
        {
            _speed = moveSpeed;
            _characterAnimationController = characterAnimationController;
        }
        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            
            _playerMovement = new Vector3(horizontal, 0, vertical);
            
            _playerMovement.Normalize();
            transform.Translate(_playerMovement * _speed * Time.deltaTime, Space.World);
            ClientSend.PlayerMovement(transform.position, _playerMovement);
            _characterAnimationController.SetSpeedToBlendTree(_playerMovement.magnitude);
            transform.forward =  _playerMovement != Vector3.zero ? _playerMovement : transform.forward;
        }
    }
}