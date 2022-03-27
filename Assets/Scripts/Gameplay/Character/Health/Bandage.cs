using Gameplay.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class Bandage : MonoBehaviour
    {
        private Health _health;
        private bool _isBandaging = false;

        public void SetParams(Health health)
        {
            _health = health;
        }
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (!_isBandaging)
                {
                    StartCoroutine(Bandaging());
                }
            }
        }
        private IEnumerator Bandaging()
        {
            _isBandaging = true;
            int bandageTime = 10;
            int heal = 10;
            while (bandageTime > 0)
            {
                yield return new WaitForSeconds(1);
                _health.ApplyHeal(heal);
                heal *= 2;
                bandageTime -= 1;
            }
            _isBandaging = false;
        }
    }
