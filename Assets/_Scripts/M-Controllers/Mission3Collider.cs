using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission3Collider : MonoBehaviour
{
    public bool canPlant;
    private void OnTriggerEnter(Collider collider)
    {
        var hit = collider.GetComponent<PlayerController>();
        if (hit != null)
        {
            GameManager.instance.canNotEnterMission.gameObject.SetActive(true);
            GameManager.instance.canNotEnterMission.text = "Press E to plant Bomb";
            StartCoroutine(GameManager.instance.hideMissionPromptText(3f));
            canPlant = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canPlant)
            {
                GameManager.instance.missionThreeBomb.SetActive(true);
                GameManager.instance.canNotEnterMission.gameObject.SetActive(true);
                GameManager.instance.canNotEnterMission.text = "Run Fast and get outside of the park";
                StaticVars.isMissionThreeComplete = true;
                StaticVars.inMission = false;
                StaticVars.showWaypointMarker = false;
                StartCoroutine(GameManager.instance.hideMissionPromptText(5f));
                StartCoroutine(initiateBlastAnimation(7f));
                
            }
            
        }
    }

    public IEnumerator initiateBlastAnimation(float timer)
    {
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene("Mission3-PostAnimate");
    }
}
