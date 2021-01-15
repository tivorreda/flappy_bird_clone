using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageableObject = other.transform.GetComponent<IDamageable>();
        damageableObject?.DoDamage(1);
    }
}
