using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100f;
    private Animator _animtor;
    public bool isDeadTrigger=false;

    private void Start()
    {
        _animtor = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(float damage,  string hitObject)
    {
        health -= damage;
        if (health <= 0)
        {
            _animtor.SetTrigger("isDead");
            isDeadTrigger = true;
            Destroy(gameObject, 2f);
            
        }

        if (!isDeadTrigger)
        {
            if (hitObject.Equals("Police"))
            {
                _animtor.SetTrigger("isShot");
            }
            if (hitObject.Equals("Peds"))
            {
                _animtor.SetTrigger("isShot");
            }
        }

    }
    
}
