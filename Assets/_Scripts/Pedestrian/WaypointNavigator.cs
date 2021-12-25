using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaypointNavigator : MonoBehaviour
{
    private PedestrianNavigationController controller;

    public Waypoint currentWaypoint;

    private int direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = Mathf.RoundToInt(Random.Range(0f, 1f));
        controller = GetComponent<PedestrianNavigationController>();
        try
        {
            controller.SetDestination(currentWaypoint.GetPosition());
        }
        catch (Exception e)
        {
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.reachedDestination)
        {
            if (controller.isPeds)
            {
                if (direction == 0)
                {
                    currentWaypoint = currentWaypoint.nextWaypoint;
                }else if (direction == 1)
                {
                    currentWaypoint = currentWaypoint.previousWaypoint;
                }
            }else if (controller.isVehicle)
            {
                bool shouldBranch = false;
                if (currentWaypoint.branches != null && currentWaypoint.branches.Count > 0)
                {
                    shouldBranch = Random.Range(0f, 1f) <= currentWaypoint.branchRatio ? true : false;
                }

                if (shouldBranch)
                {
                    currentWaypoint = currentWaypoint.branches[Random.Range(0, currentWaypoint.branches.Count - 1)];
                }
                else
                {
                    if (currentWaypoint.nextWaypoint != null)
                    {
                        currentWaypoint = currentWaypoint.nextWaypoint;
                    }
                    else
                    {
                        currentWaypoint = currentWaypoint.previousWaypoint;
                    }
                    
                }
                
                
                
            }
            
            
            controller.SetDestination(currentWaypoint.GetPosition());
        }
        
    }
}
