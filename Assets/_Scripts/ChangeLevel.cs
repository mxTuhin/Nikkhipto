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

    void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
