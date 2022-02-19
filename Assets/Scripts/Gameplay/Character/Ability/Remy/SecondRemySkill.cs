using Gameplay.Character.MovementControllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondRemySkill : Ability
{
    private bool _onCharge;
    [SerializeField] private int _damage;
    [SerializeField] private float _chargeSpeed;
    [SerializeField] private HPContoller _hp;
    public void Update()
    {
        if (_onCharge)
        {
            transform.position += transform.forward * _chargeSpeed * Time.deltaTime;
        }
    }
    public override void OnPress()
    {
        if (!_onCooldown)
        {
            base.OnPress();
            RotateCharacaterByTheMouse();
            StartCoroutine(Charging());
        }
    }
    private void RotateCharacaterByTheMouse()
    {
        Plane playerplane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist;

        if (playerplane.Raycast(ray, out hitdist))
        {
            Vector3 targetpoint = ray.GetPoint(hitdist);
            Quaternion targetrotation = Quaternion.LookRotation(targetpoint - transform.position);
            transform.rotation = targetrotation;
        }
    }
    private IEnumerator Charging()
    {
        _hp.isImmune = true;
        _onCharge = true;
        Debug.Log("start Charge");
        yield return new WaitForSeconds(0.15f);
        _onCharge = false;
        _hp.isImmune = false;
        Debug.Log("stop Charge");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_onCharge)
        {
            if (other.TryGetComponent(out ZombieMelee zombie))
            {
                zombie.ApplyDamage(_damage);
            }
        }
    }
}
