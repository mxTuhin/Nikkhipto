using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    private float health;
    private Animator _animtor;
    public bool isDeadTrigger=false;
    public SpriteRenderer healthBar;
    public GameObject playerCharacter;

    public float shotGrace;

    private void Start()
    {
        health = maxHealth;
        _animtor = GetComponentInChildren<Animator>();
        playerCharacter = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (healthBar.gameObject.activeSelf)
        {
            if (Time.time - shotGrace >= 3)
            {
                healthBar.gameObject.SetActive(false);
            }
        }
    }

    public void TakeDamage(float damage,  string hitObject)
    {
        shotGrace = Time.time;
        health -= damage;
        if (health <= 0)
        {
            if (!isDeadTrigger)
            {
                _animtor.SetTrigger("isDead");
                isDeadTrigger = true;
            }
            healthBar.size = new Vector2(0,0f);
            if (hitObject.Equals("LeaderOne"))
            {
                Destroy(gameObject, 3f);
                SelfObjectDestroyer.instance.destroyMissionOneComponents(30f);
                GameManager.instance.missionPassedText[0].SetActive(true);
                StaticVars.inMission = false;
                StaticVars.isMissionOneComplete = true;
                StaticVars.showWaypointMarker = false;
            }

            if (hitObject.Equals("Player"))
            {
                StartCoroutine(respawnPLayer());
            }
            else
            {
                Destroy(gameObject, 2f);
            }
            
            
        }

        if (!isDeadTrigger)
        {
            // healthBar.transform.Rotate(0, playerCharacter.transform.position.y, 0);
            if (hitObject.Equals("Police"))
            {
                if (!healthBar.gameObject.activeSelf)
                {
                    healthBar.gameObject.SetActive(true);
                }
                healthBar.size -= new Vector2(0,0.85f);
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

            if (hitObject.Equals("Player"))
            {
                if (!healthBar.gameObject.activeSelf)
                {
                    healthBar.gameObject.SetActive(true);
                }
                healthBar.size -= new Vector2(0,0.0512f);
                if (healthBar.size.y <= 1.3)
                {
                    healthBar.color=Color.red;
                }
                // gameObject.transform.LookAt(playerCharacter.transform);
            }
            
        }
        

    }

    public void addHealth()
    {
        health = 500;
        healthBar.size = new Vector2(2.56f, 2.56f);
        healthBar.color = new Color(0, 255, 196, 255);
        if (!healthBar.gameObject.activeSelf)
        {
            healthBar.gameObject.SetActive(true);
        }
        shotGrace = Time.time;
    }

    public IEnumerator respawnPLayer()
    {
        GameManager.instance.sceneChangeBackground.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("PrimaryScene");
    }
    
}
