using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FootSteps : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;

    
    private CharacterController cc;

    private void Awake()
    {
        
        cc = GetComponent<CharacterController>();
    }

    
    
    public void WalkStepAgain()
    {
        AudioClip clip = GetRandomClip();
        // _audioSource.PlayOneShot(clip);
    }


    private AudioClip GetRandomClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
