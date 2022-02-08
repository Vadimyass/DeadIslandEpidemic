using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    private int _level;
    private float _cooldown;
    private int _name;
    private int _description;

    public virtual void OnPress()
    {
        
    }

    public virtual void OnCooldownEnd()
    {
        
    }
}
