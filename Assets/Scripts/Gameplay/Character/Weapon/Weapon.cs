using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public string _name;
    [SerializeField] public float damage;
    public float damageMultiplier = 1.0f;
    public float realDamage => damage * damageMultiplier;
    [SerializeField] public int attackRange;
}
