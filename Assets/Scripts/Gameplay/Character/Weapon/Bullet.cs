using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _rb;
    private float _damage;
    
    public void OnStart(float damage, int range)
    {
        _damage = damage;
        _rb.velocity = transform.right * _speed;
        Destroy(gameObject, range/_speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out ZombieMelee zombie))
        {
            zombie.ApplyDamage((int)_damage);
            Destroy(gameObject);
        }
    }
}
