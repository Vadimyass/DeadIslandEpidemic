using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInAbilityAreaTrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out Outline outline))
        {
            outline.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Outline outline))
        {
            outline.enabled = false;
        }
    }
}
