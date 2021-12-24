using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCollider : MonoBehaviour
{
    public bool mission1;
    public bool mission2;
    public bool mission3;
    public bool mission4;
    public bool mission5;

    private void OnTriggerEnter(Collider collider)
    {
        var hit = collider.GetComponent<PlayerController>();
        if (hit != null)
        {
            if (mission1)
            {
                print("Mission 01");
            }
            else if (mission2)
            {
                print("Mission 02");
            }
            else if (mission3)
            {
                print("Mission 03");
            }
            else if (mission4)
            {
                print("Mission 04");
            }
            else if (mission5)
            {
                print("Mission 05");
            }
            
        }
    }
}
