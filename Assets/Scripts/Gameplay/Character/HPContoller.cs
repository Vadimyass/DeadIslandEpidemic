using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPContoller : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    private float _currentHealth;
    [SerializeField] private Image _healthBar;
    public bool isImmune = false;
    private void Start()
    {
        _currentHealth = _maxHealth;
    }
    public void ApplyDamage(int damage)
    {
        if (!isImmune)
        {
            _currentHealth -= damage;
            _healthBar.fillAmount = _currentHealth / _maxHealth;
            Debug.Log(_currentHealth);
        }
        else
        {
            Debug.Log("blocked damage");
        }
    }
}
