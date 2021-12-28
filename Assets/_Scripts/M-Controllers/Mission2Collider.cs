using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission2Collider : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        var hit = collider.GetComponent<PlayerController>();
        if (hit != null)
        {
            CouroutineLogics();
            GameManager.instance.missionPassedText[1].SetActive(true);
            StaticVars.inMission = false;
            StaticVars.isMissionTwoComplete = true;
            StaticVars.showWaypointMarker = false;
            Destroy(gameObject);
            
        }
    }

    void CouroutineLogics()
    {
        GameManager.instance.initiatePhoneCall();
    }

    
}
