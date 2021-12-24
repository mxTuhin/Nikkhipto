using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionControllers : MonoBehaviour
{
    public GameObject player;

    public GameObject police;

    private Animator playerAnimator;

    private Animator policeAnimator;

    private PlayerController playerController;

    private Police policeController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        policeController = police.GetComponent<Police>();
        playerAnimator = player.GetComponentInChildren<Animator>();
        policeAnimator = police.GetComponentInChildren<Animator>();
        StartCoroutine(playerTurnOffAnimation());
        StartCoroutine(policeTurnOffAnimation());
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
}
