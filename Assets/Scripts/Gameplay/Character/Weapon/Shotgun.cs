using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] private GameObject _shotStart;
    [SerializeField] private GameObject _bullet;
    
    
    public void Shoot(Transform hero)
    {
        Instantiate(_bullet, 
            new Vector3(_shotStart.transform.position.x,
            _shotStart.transform.position.y,
            _shotStart.transform.position.z),
            hero.rotation);
    }
}
