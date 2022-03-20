using System;
using UnityEngine;

namespace Gameplay.CameraController
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform _player;
        private Vector3 cameraOffset;

        [Range(0.01f, 1.0f)] 
        public float smoothness = 0.5f;


        public void SetParams(Transform player)
        {
            _player = player;
        }
        //private void Start()
        //{
        //    cameraOffset = transform.position - _player.position;
        //}

        //private void Update()
        //{
        //    Vector3 newPos = _player.position + cameraOffset;
        //    transform.position = Vector3.Slerp(transform.position, newPos,smoothness);
        //}
    }
}