using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab;

    private int carToSpawn;

    public GameObject carSpawnHolder;

    private int previousPoint=0;
    // Start is called before the first frame update
    void Start()
    {
        carToSpawn = 10;
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        int count = 0;
        while (count < carToSpawn)
        {
            GameObject obj = Instantiate((carPrefab));
            int randomPoint = Random.Range(0, transform.childCount - 1);
            if (randomPoint==previousPoint)
            {
                randomPoint = Mathf.Abs(Random.Range(0, transform.childCount - 1) - Random.Range(0,5)) ;
                previousPoint = randomPoint;
            }
            Transform child = transform.GetChild(randomPoint);
            obj.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
            obj.transform.position = child.position+new Vector3(0,3f,0);
            try
            {
                obj.transform.SetParent(carSpawnHolder.transform);
            }
            catch (Exception e)
            {
                
            }



            yield return new WaitForEndOfFrame();
            count ++ ;
        }
    }
}
