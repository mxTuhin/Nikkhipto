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
    public GameObject playerCharacter;

    private void Start()
    {
        _animtor = GetComponentInChildren<Animator>();
        playerCharacter = GameObject.FindGameObjectWithTag("Player");
    }

    public void TakeDamage(float damage,  string hitObject)
    {
        health -= damage;
        if (health <= 0)
        {
            _animtor.SetTrigger("isDead");
            isDeadTrigger = true;
            healthBar.size = new Vector2(0,0f);
            if (hitObject.Equals("LeaderOne"))
            {
                Destroy(gameObject, 3f);
                SelfObjectDestroyer.instance.destroyMissionOneComponents(30f);
            }
            else
            {
                Destroy(gameObject, 2f);
            }
            
            
        }

        if (!this.isDeadTrigger)
        {
            // healthBar.transform.Rotate(0, playerCharacter.transform.position.y, 0);
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
                gameObject.transform.LookAt(playerCharacter.transform);
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
                // gameObject.transform.LookAt(playerCharacter.transform);
                _animtor.SetTrigger("isShot");
                
                
            }
            
            if (hitObject.Equals("LeaderOne"))
            {
                gameObject.GetComponent<AudioSource>().Stop();
                if (!healthBar.gameObject.activeSelf)
                {
                    healthBar.gameObject.SetActive(true);
                }
                healthBar.size -= new Vector2(0,1.3f);
                if (healthBar.size.y <= 1.3)
                {
                    healthBar.color=Color.red;
                }
            }
            
        }
        

    }
    
}
