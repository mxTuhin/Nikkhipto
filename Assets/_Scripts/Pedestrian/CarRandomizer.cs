using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRandomizer : MonoBehaviour
{
    public GameObject[] car;
    // Start is called before the first frame update
    void Start()
    {
        car[Random.Range(0, car.Length)].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
