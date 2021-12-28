using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool gameOver;

    public bool pauseMenuState;

    public GameObject missionOneObjects;
    public GameObject missionThreeObjects;
    public GameObject missionTwoObjects;
    public GameObject missionFourObjects;

    public AudioSource missionFourAudio;

    public bool missionFourEndTrigger;
    
    
    
    public GameObject missionThreeBomb;

    public GameObject sceneChangeBackground;

    public GameObject[] missionSelectors;

    public GameObject[] playerSpawns;
    public GameObject playerMissionThreeSpawn;

    public GameObject[] missionPassedText;
    public GameObject[] missionMinimapIcons;

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

    public AudioSource talkingAudioSource;

    public bool phoneCallTrigger;

    public bool canControl;

    


    // Start is called before the first frame update
    void Start()
    {
        
        instance = this;
        canControl = true;
        

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

        if (StaticVars.isMissionThreeComplete && !StaticVars.isMissionFourTriggered)
        {
            
            missionPassedText[2].SetActive(true);
            player.transform.position = new Vector3(playerMissionThreeSpawn.transform.position.x,
                playerMissionThreeSpawn.transform.position.y, playerMissionThreeSpawn.transform.position.z);
            
        }

        if (StaticVars.isMissionThreeComplete)
        {
            preMissionThreeObject.SetActive(false);
            postMissionThreeObject.SetActive(true);
        }
        
        if (StaticVars.isMissionFourTriggered && !StaticVars.isMissionFourComplete)
        {
            player.transform.position = new Vector3(playerSpawns[3].transform.position.x,
                playerSpawns[3].transform.position.y, playerSpawns[3].transform.position.z);
            missionFourObjects.SetActive(true);
            canNotEnterMission.gameObject.SetActive(true);
            canNotEnterMission.text = "Go To Abandoned Arena";
            
            StartCoroutine(hideMissionPromptText(5f));
        }
        
        else if (StaticVars.isMissionThreeTriggered && !StaticVars.isMissionThreeComplete)
        {
            player.transform.position = new Vector3(playerSpawns[1].transform.position.x,
                playerSpawns[1].transform.position.y, playerSpawns[1].transform.position.z);
            missionThreeObjects.SetActive(true);
            canNotEnterMission.gameObject.SetActive(true);
            canNotEnterMission.text = "Go To the Park At the Straight Right Corner";
            
            StartCoroutine(hideMissionPromptText(5f));
        }
        
        else if (StaticVars.isMissionTwoTriggered && !StaticVars.isMissionTwoComplete)
        {
            player.transform.position = new Vector3(playerSpawns[2].transform.position.x,
                playerSpawns[2].transform.position.y, playerSpawns[2].transform.position.z);
            missionTwoObjects.SetActive(true);
            canNotEnterMission.gameObject.SetActive(true);
            canNotEnterMission.text = "Bring the Money Machine";
            
            StartCoroutine(hideMissionPromptText(5f));
        }
        
        else if (StaticVars.isMissionOneTriggered && !StaticVars.isMissionOneComplete)
        {
            player.transform.position = new Vector3(playerSpawns[0].transform.position.x, playerSpawns[0].transform.position.y, playerSpawns[0].transform.position.z) ;
            missionOneObjects.SetActive(true);
            canNotEnterMission.gameObject.SetActive(true);
            canNotEnterMission.text = "Go In Front of Stage and Kill Elias";
            
            StartCoroutine(hideMissionPromptText(5f));
            
        }

        if (StaticVars.showInfo)
        {
            sceneInfo.SetActive(true);
        }
        else
        {
            sceneInfo.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 1.2f);
        
        if (StaticVars.isMissionFourTriggered)
        {
            if (missionFourEndTrigger)
            {
                cineMachineCamera.SetActive(false);
                if (missionFourAudio.isPlaying==false)
                {
                    missionFourAudio.Play();
                }
                
                float step = 1.0f * Time.deltaTime;
                mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position,
                    mainCamera.transform.position + new Vector3(0, 10f, 0), step);
                mainCamera.transform.Rotate(new Vector3(mainCamera.transform.rotation.x,mainCamera.transform.rotation.y+step,mainCamera.transform.rotation.z), step*10);
                // mainCamera.transform.LookAt(player.transform);
                StartCoroutine(changeScene());

            }
        }

        
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            QualitySettings.SetQualityLevel(0, true);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            QualitySettings.SetQualityLevel(1, true);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            QualitySettings.SetQualityLevel(2, true);
        }

        if (Input.GetKey(KeyCode.X))
        {
            triggerPhoneCall();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!sceneInfo.activeSelf)
            {
                sceneInfo.SetActive(true);
                StaticVars.showInfo = true;
            }
            else
            {
                sceneInfo.SetActive(false);
                StaticVars.showInfo = false;
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

    public IEnumerator changeScene()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene("Menu");
    }

    public void initiatePhoneCall()
    {
        StartCoroutine(deactivateMissionTwoObjects());
        StartCoroutine(triggerPhoneCall());
        
    }

    public IEnumerator triggerPhoneCall()
    {
        print("Phone Call");
        yield return new WaitForSeconds(5f);
        talkingAudioSource.Play();
        canControl = false;
        player.GetComponentInChildren<Animator>().SetTrigger("isTalking");
        StartCoroutine(canBeAbleToRun());
    }

    IEnumerator canBeAbleToRun()
    {
        print("Run");
        yield return new WaitForSeconds(19f);
        canControl = true;
    }
    
    IEnumerator deactivateMissionTwoObjects()
    {
        print("Deactivating");
        yield return new WaitForSeconds(5f);
        missionTwoObjects.SetActive(false);
    }

}
