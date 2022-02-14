using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private int _damage;
    void Start()
    {
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
