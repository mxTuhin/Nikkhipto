using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        var hit = collider.GetComponent<PlayerController>();
        if (hit != null)
        {
            hit.GetComponent<Health>().addHealth();
            Destroy(gameObject);
        }
    }
}
