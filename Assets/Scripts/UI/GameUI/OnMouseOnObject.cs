using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseOnObject : MonoBehaviour
{
    [SerializeField] private Outline _outline;
    private void OnMouseEnter()
    {
        _outline.enabled = true;
    }
    private void OnMouseExit()
    {
        _outline.enabled = false;
    }
}
