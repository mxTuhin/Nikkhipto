using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class MissionWaypoint : MonoBehaviour
{
    public Transform[] missionTransforms;
    public static MissionWaypoint instance;

    public Image waypointMarker;

    public int missionNumber=0;

    public Text distance;

    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        if (StaticVars.showWaypointMarker)
        {
            waypointMarker.gameObject.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        
        float minX = waypointMarker.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = waypointMarker.GetPixelAdjustedRect().width / 2;
        float maxY = Screen.height - minY;

        
        
        Vector2 pos = Camera.main.WorldToScreenPoint(missionTransforms[StaticVars.missionSelector].position+offset);
        if (Vector3.Dot(missionTransforms[missionNumber].position - transform.position, transform.forward) < 0)
        {
            if (pos.x < Screen.width / 2)
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX;
            }
        }
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        waypointMarker.transform.position = pos;
        distance.text = (int)Vector3.Distance(missionTransforms[StaticVars.missionSelector].position, transform.position)+"";
        
        if((int)Vector3.Distance(missionTransforms[StaticVars.missionSelector].position, transform.position)<=10)
        {
            waypointMarker.gameObject.SetActive(false);
        }

    }
}
