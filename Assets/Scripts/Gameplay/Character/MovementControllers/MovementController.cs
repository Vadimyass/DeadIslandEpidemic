using System;
using System.Numerics;
using Gameplay.Character.AnimationControllers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using Plane = UnityEngine.Plane;
using static Gameplay.Character.CombatController;

namespace Gameplay.Character.MovementControllers
{
    public class MovementController : MonoBehaviour
    {
        private float _speed;

        private CharacterAnimationController _characterAnimationController;
        
        private Vector3 _playerMovement;
        public void RotateCharacaterByTheMouse()
        {
            Plane playerplane = new Plane(Vector3.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitdist;

            if (playerplane.Raycast(ray, out hitdist))
            {
                Vector3 targetpoint = ray.GetPoint(hitdist);
                Quaternion targetRotation = Quaternion.LookRotation(targetpoint - transform.position);
                transform.rotation = targetRotation;
                ClientSend.SendPlayerRotation(transform.rotation);
            }
        }
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