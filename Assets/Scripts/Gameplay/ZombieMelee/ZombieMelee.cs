using System.Collections;
using System.Collections.Generic;
using Gameplay.Character.MovementControllers;
using Gameplay.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Gameplay.Character.Leveling;

public class ZombieMelee : MonoBehaviour,ITargetable
{
    public int Health;
    [SerializeField] private int _damage;
    private int _xpFromDeath = 200;
    public void ApplyDamage(int damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            Death();
        }
        Debug.Log(damage);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HPContoller player))
        {
            player.ApplyDamage(_damage);
        }
    }

    private void Death()
    {
        Destroy(gameObject);
        Collider[] hitColliders = Physics.OverlapSphere(this.gameObject.transform.position, 5);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.TryGetComponent(out HeroLeveling player))
            {
                player.TakeXP(_xpFromDeath);
            }
        }
    }
}
