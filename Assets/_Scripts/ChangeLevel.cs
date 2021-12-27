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
        Invoke("changeScene", timer);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("PrimaryScene");
        }
    }

    void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
