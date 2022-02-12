using System.Collections;
using System.Collections.Generic;
using Gameplay.Interfaces;
using UnityEngine;

public class ZombieMelee : MonoBehaviour,ITargetable
{
    public int Health;

    public void ApplyDamage(int damage)
    {
        Health -= damage;
        Debug.Log(Health);
    }
}
