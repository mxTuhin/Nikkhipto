
using UnityEngine;
using UnityEngine.SceneManagement;

public class LauncherManager : MonoBehaviour
{
    public GameObject mainCanvas;

    public GameObject keyCanvas;

    public GameObject credCanvas;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    // Update is called once per frame

    public void playGame()
    {
        SceneManager.LoadScene("PrimaryScene");
    }
    
    
    public void keyCanvasInit()
    {
        mainCanvas.SetActive(false);
        keyCanvas.SetActive(true);
    }

    public void mainCanvasInit()
    {
        mainCanvas.SetActive(true);
        keyCanvas.SetActive(false);
        credCanvas.SetActive(false);
    }

    public void creditCanvas()
    {
        mainCanvas.SetActive(false);
        credCanvas.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
