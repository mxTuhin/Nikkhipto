using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            MissionWaypoint.instance.waypointMarker.gameObject.SetActive(true);
            if (mission1)
            {
                print("Mission 01");
                StaticVars.missionSelector = 0;
                SceneManager.LoadScene("Mission1Animate");
            }
            else if (mission2)
            {
                print("Mission 02");
                StaticVars.missionSelector = 1;
            }
            else if (mission3)
            {
                print("Mission 03");
                StaticVars.missionSelector = 2;
            }
            else if (mission4)
            {
                print("Mission 04");
                StaticVars.missionSelector = 3;
            }
            else if (mission5)
            {
                print("Mission 05");
                StaticVars.missionSelector = 4;
            }
            
        }
    }
}
