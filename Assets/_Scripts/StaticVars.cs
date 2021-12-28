using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using UnityEngine;

public class StaticVars : MonoBehaviour
{
    public static int missionSelector=0;

    public static bool isMissionOneTriggered=true;
    public static bool isMissionTwoTriggered=false;
    public static bool isMissionThreeTriggered=false;
    public static bool isMissionFourTriggered=false;

    public static bool isMissionOneComplete = true;
    public static bool isMissionTwoComplete = false;
    
    public static bool isMissionThreeComplete = false;
    public static bool isMissionFourComplete = false;
    
    public static bool showWaypointMarker=false;

    public static bool inMission;

    public static bool showInfo = true;

}
