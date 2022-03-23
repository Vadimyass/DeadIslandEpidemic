using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameUI
{
    public class CharactersStatusView : MonoBehaviour
    {
        [SerializeField] public Image healthBar;
        private void LateUpdate()
        {
            transform.eulerAngles = Camera.main.transform.eulerAngles;
        }

        public void CharacterChangeHealth(float healthPercentage)
        {
            healthBar.fillAmount = healthPercentage;
        }
    }
}
