using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianSpawner : MonoBehaviour
{
    public GameObject pedestrianPrefab;

    private int pedestrianToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        pedestrianToSpawn = 1;
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
            GameObject obj = Instantiate((pedestrianPrefab));
            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            obj.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
            obj.transform.position = child.position;

            yield return new WaitForEndOfFrame();
            count ++ ;
        }
    }
}
