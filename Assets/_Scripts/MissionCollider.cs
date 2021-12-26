using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionCollider : MonoBehaviour
{
    public bool mission1;
    public bool mission2;
    public bool mission3;
    public bool mission4;
    public bool mission5;
    
    

    private void OnTriggerEnter(Collider collider)
    {
        
        var hit = collider.GetComponent<PlayerController>();
                    if (hit != null)
                    {
                        
                        if (!StaticVars.inMission)
                        {
                            
                            if (mission1)
                            {
                                print("Mission 01");
                                StaticVars.missionSelector = 0;
                                StaticVars.isMissionOneTriggered = true;
                                GameManager.instance.sceneChangeBackground.SetActive(true);
                                GameManager.instance.missionNameImages[StaticVars.missionSelector].SetActive(true);
                                StaticVars.inMission = true;
                                StartCoroutine(changeScene("Mission1Animate"));
                                
                
                            }
                            else if (mission2)
                            {
                                if (StaticVars.isMissionOneTriggered)
                                {
                                    print("Mission 02");
                                    StaticVars.missionSelector = 1;
                                    StaticVars.isMissionTwoTriggered = true;
                                    GameManager.instance.sceneChangeBackground.SetActive(true);
                                    GameManager.instance.missionNameImages[StaticVars.missionSelector].SetActive(true);
                                    StaticVars.inMission = true;
                                    StartCoroutine(changeScene("Mission2Animate"));
                                }
                                else
                                {
                                    GameManager.instance.canNotEnterMission.gameObject.SetActive(true);
                                    GameManager.instance.canNotEnterMission.text = "Complete Mission A First";
                                    StartCoroutine(hideMissionPromptText());
                                }
                                
                            }
                            else if (mission3)
                            {
                                if (StaticVars.isMissionTwoTriggered)
                                {
                                    print("Mission 03");
                                    StaticVars.missionSelector = 2;
                                    StaticVars.isMissionThreeTriggered = true;
                                    GameManager.instance.sceneChangeBackground.SetActive(true);
                                    GameManager.instance.missionNameImages[StaticVars.missionSelector].SetActive(true);
                                    StaticVars.inMission = true;
                                    StartCoroutine(changeScene("Mission3Animate"));
                                }
                                else
                                {
                                    GameManager.instance.canNotEnterMission.gameObject.SetActive(true);
                                    GameManager.instance.canNotEnterMission.text = "Complete Mission B First";
                                    StartCoroutine(hideMissionPromptText());
                                }
                                
                            }
                            else if (mission4)
                            {
                                if (StaticVars.isMissionFourTriggered)
                                {
                                    print("Mission 04");
                                    StaticVars.missionSelector = 3;
                                    GameManager.instance.sceneChangeBackground.SetActive(true);
                                    GameManager.instance.missionNameImages[StaticVars.missionSelector].SetActive(true);
                                    StaticVars.inMission = true;
                                }
                                else
                                {
                                    GameManager.instance.canNotEnterMission.gameObject.SetActive(true);
                                    GameManager.instance.canNotEnterMission.text = "Complete Mission C First";
                                    StartCoroutine(hideMissionPromptText());
                                }
                                
                            }
                            else if (mission5)
                            {
                                print("Mission 05");
                                StaticVars.missionSelector = 4;
                            }
            
                        }
                        else
                        {
                            GameManager.instance.canNotEnterMission.gameObject.SetActive(true);
                            GameManager.instance.canNotEnterMission.text = "Complete the mission First";
                            StartCoroutine(hideMissionPromptText());
                        }
                        
                    }

    }

    IEnumerator changeScene(string missionName)
    {
        MissionWaypoint.instance.waypointMarker.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(missionName);
    }

    IEnumerator hideMissionPromptText()
    {
        yield return new WaitForSeconds(3f);
        GameManager.instance.canNotEnterMission.gameObject.SetActive(false);
    }
}
