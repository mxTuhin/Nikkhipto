using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PedestrianSpawner : MonoBehaviour
{
    public GameObject pedestrianPrefab;

    private int pedestrianToSpawn;

    public GameObject pedestrianHolder;
    // Start is called before the first frame update
    void Start()
    {
        pedestrianToSpawn = 20;
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        int count = 0;
        while (count < pedestrianToSpawn)
        {
            GameObject obj = Instantiate(pedestrianPrefab);
            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            obj.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
            obj.transform.position = child.position;
            // try
            // {
            //     obj.transform.SetParent(pedestrianHolder.transform);
            // }
            // catch (Exception e)
            // {
            //     
            // }

            

            yield return new WaitForEndOfFrame();
            count ++ ;
        }
    }
}
