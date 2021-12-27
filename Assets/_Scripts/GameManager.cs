using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool gameOver;

    public bool pauseMenuState;

    public GameObject missionOneObjects;
    public GameObject missionThreeObjects;
    public GameObject missionTwoObjects;
    
    public GameObject missionThreeBomb;

    public GameObject sceneChangeBackground;

    public GameObject[] missionSelectors;

    public GameObject[] playerSpawns;
    public GameObject playerMissionThreeSpawn;

    public GameObject[] missionPassedText;

    public GameObject[] missionNameImages;

    public GameObject player;

    public Text canNotEnterMission;
    public ParticleSystem[] explosionParticles;


    public GameObject preMissionThreeObject;
    public GameObject postMissionThreeObject;

    public GameObject cineMachineCamera;
    public GameObject mainCamera;
    public GameObject animateCamera;

    public GameObject sceneInfo;


    


    // Start is called before the first frame update
    void Start()
    {
        
        instance = this;
        if (StaticVars.isMissionOneTriggered && !StaticVars.isMissionOneComplete)
        {
            player.transform.position = new Vector3(playerSpawns[0].transform.position.x, playerSpawns[0].transform.position.y, playerSpawns[0].transform.position.z) ;
            missionOneObjects.SetActive(true);
            canNotEnterMission.gameObject.SetActive(true);
            canNotEnterMission.text = "Go In Front of Stage and Kill Elias";
            
            StartCoroutine(hideMissionPromptText(5f));
            
        }

        if (StaticVars.isMissionTwoTriggered && !StaticVars.isMissionTwoComplete)
        {
            player.transform.position = new Vector3(playerSpawns[2].transform.position.x,
                playerSpawns[2].transform.position.y, playerSpawns[2].transform.position.z);
            missionTwoObjects.SetActive(true);
            canNotEnterMission.gameObject.SetActive(true);
            canNotEnterMission.text = "Bring the Money Machine";
            
            StartCoroutine(hideMissionPromptText(5f));
        }

        if (StaticVars.isMissionThreeTriggered && !StaticVars.isMissionThreeComplete)
        {
            player.transform.position = new Vector3(playerSpawns[1].transform.position.x,
                playerSpawns[1].transform.position.y, playerSpawns[1].transform.position.z);
            missionThreeObjects.SetActive(true);
            canNotEnterMission.gameObject.SetActive(true);
            canNotEnterMission.text = "Go To the Park At the Straight Right Corner";
            
            StartCoroutine(hideMissionPromptText(5f));
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

        if (StaticVars.isMissionThreeComplete)
        {
            preMissionThreeObject.SetActive(false);
            postMissionThreeObject.SetActive(true);
            missionPassedText[2].SetActive(true);
            player.transform.position = new Vector3(playerMissionThreeSpawn.transform.position.x,
                playerMissionThreeSpawn.transform.position.y, playerMissionThreeSpawn.transform.position.z);
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 1.2f);

        
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            QualitySettings.SetQualityLevel(0, true);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            QualitySettings.SetQualityLevel(1, true);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            QualitySettings.SetQualityLevel(2, true);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!sceneInfo.activeSelf)
            {
                sceneInfo.SetActive(true);
            }
            else
            {
                sceneInfo.SetActive(false);
            }
        }

    }

    IEnumerator ExplodeParticles(float timer, ParticleSystem explot)
    {
        yield return new WaitForSeconds(timer);
        explot.Play();
    }
    
    public IEnumerator hideMissionPromptText(float timer)
    {
        yield return new WaitForSeconds(timer);
        GameManager.instance.canNotEnterMission.gameObject.SetActive(false);
    }
    
}
