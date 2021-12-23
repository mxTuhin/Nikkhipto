using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    private PedestrianNavigationController controller;

    public Waypoint currentWaypoint;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PedestrianNavigationController>();
        controller.SetDestination(currentWaypoint.GetPosition());
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.reachedDestination)
        {
            currentWaypoint = currentWaypoint.nextWaypoint;
            controller.SetDestination(currentWaypoint.GetPosition());
        }
        
    }
}
