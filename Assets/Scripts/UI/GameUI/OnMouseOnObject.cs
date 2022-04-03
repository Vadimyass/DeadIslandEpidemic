using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class OnMouseOnObject : MonoBehaviour
{
    [SerializeField] private Outline _outline;
    private void OnMouseEnter()
    {
        if(_outline.IsNull())
            return;
         
        _outline.enabled = true;
    }
    private void OnMouseExit()
    {
        if (_outline.IsNull())
            return;

        _outline.enabled = false;
    }
}
