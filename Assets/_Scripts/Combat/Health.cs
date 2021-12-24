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
    public SpriteRenderer healthBar;

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
                if (!healthBar.gameObject.activeSelf)
                {
                    healthBar.gameObject.SetActive(true);
                }
                healthBar.size -= new Vector2(0,0.256f);
                if (healthBar.size.y <= 1.3)
                {
                    healthBar.color=Color.red;
                }
                _animtor.SetTrigger("isShot");
            }
            if (hitObject.Equals("Peds"))
            {
                if (!healthBar.gameObject.activeSelf)
                {
                    healthBar.gameObject.SetActive(true);
                }
                healthBar.size -= new Vector2(0,0.256f);
                if (healthBar.size.y <= 1.3)
                {
                    healthBar.color=Color.red;
                }
                _animtor.SetTrigger("isShot");
                
                
            }
        }

    }
    
}
