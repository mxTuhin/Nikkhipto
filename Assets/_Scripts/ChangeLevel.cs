using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    public string sceneName;

    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(changeScene(timer));
        Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("PrimaryScene");
        }
    }

    IEnumerator changeScene(float timer)
    {
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene(sceneName);
    }
}
