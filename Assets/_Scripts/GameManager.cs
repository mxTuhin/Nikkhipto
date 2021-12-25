using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool gameOver;

    public bool pauseMenuState;

    public GameObject missionOneObjects;

    public GameObject sceneChangeBackground;

    public GameObject[] missionSelectors;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        if (StaticVars.isMissionOneTriggered)
        {
            missionOneObjects.SetActive(true);
        }

        if (StaticVars.isMissionOneTriggered)
        {
            missionSelectors[0].SetActive(false);
        }

        if (StaticVars.isMissionTwoTriggered)
        {
            missionSelectors[1].SetActive(false);
        }

        if (StaticVars.isMissionThreeTriggered)
        {
            missionSelectors[2].SetActive(false);
        }

        if (StaticVars.isMissionFourTriggered)
        {
            missionSelectors[3].SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
