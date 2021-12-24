using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100f;
    private Animator _animtor;

    private void Start()
    {
        _animtor = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            
            _animtor.SetBool("isDead", true);
            Destroy(gameObject, 2f);
        }
    }
    
}
