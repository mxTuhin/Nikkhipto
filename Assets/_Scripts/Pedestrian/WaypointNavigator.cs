using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        controller.SetDestination(currentWaypoint.GetPosition());
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.reachedDestination)
        {
            if (direction == 0)
            {
                currentWaypoint = currentWaypoint.nextWaypoint;
            }else if (direction == 1)
            {
                currentWaypoint = currentWaypoint.previousWaypoint;
            }
            
            controller.SetDestination(currentWaypoint.GetPosition());
        }
        
    }
}
