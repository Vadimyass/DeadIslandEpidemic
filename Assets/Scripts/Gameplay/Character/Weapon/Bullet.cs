using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _rb;
    private int _damage;
    
    public void OnStart(int damage)
    {
        _damage = damage;
        _rb.velocity = transform.forward * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out ZombieMelee zombie))
        {
            zombie.ApplyDamage(_damage);
            Destroy(gameObject);
        }
    }
}
