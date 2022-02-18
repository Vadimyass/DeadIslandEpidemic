using System.Collections;
using System.Collections.Generic;
using Gameplay.Character.MovementControllers;
using Gameplay.Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class ZombieMelee : MonoBehaviour,ITargetable
{
    public int Health;
    [SerializeField] private int _damage;
    public void ApplyDamage(int damage)
    {
        Health -= damage;
        Debug.Log(Health);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out MovementController player))
        {
            player.ApplyDamage(_damage);
        }
    }
}
