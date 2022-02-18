using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    private int _level;
    [SerializeField] private float _cooldown;
    private int _name;
    private int _description;
    public bool _onCooldown;
    public virtual void OnPress()
    {
        StartCoroutine(OnCooldown());
    }

    private IEnumerator OnCooldown()
    {
        Debug.Log("on Cooldown");
        _onCooldown = true;
        yield return new WaitForSeconds(_cooldown);
        OnEndCooldown();
    }

    public virtual void OnEndCooldown()
    {
        _onCooldown = false;
        Debug.Log("coolDowned");
    }
}
