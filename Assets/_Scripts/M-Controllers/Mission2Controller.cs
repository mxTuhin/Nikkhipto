using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission2Controller : MonoBehaviour
{
    public GameObject player;

    public GameObject police;

    private Animator playerAnimator;

    private Animator policeAnimator;

    private PlayerController playerController;

    private Police policeController;

    public GameObject playerGun;

    public GameObject policeGun;
    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        policeController = police.GetComponent<Police>();
        playerAnimator = player.GetComponentInChildren<Animator>();
        policeAnimator = police.GetComponentInChildren<Animator>();
        StartCoroutine(lookToplayer());

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    IEnumerator playerTurnOffAnimation()
    {
        yield return new WaitForSeconds(2.0f);
        playerAnimator.SetTrigger("isIdle");
    }
    

    IEnumerator policeTurnOffAnimation()
    {
        yield return new WaitForSeconds(7.0f);
        policeAnimator.SetTrigger("isIdle");
    }

    IEnumerator swapGun()
    {
        
        yield return new WaitForSeconds(58f);
        policeGun.SetActive(false);
        playerGun.SetActive(true);
        
    }

    IEnumerator lookToplayer()
    {
        StaticVars.showWaypointMarker = true;
        yield return new WaitForSeconds(22f);
        police.transform.LookAt(player.transform);
    }
}