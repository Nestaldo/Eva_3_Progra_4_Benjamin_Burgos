using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
    public Canvas mainMenuCanvas;
    public Canvas logicAccCanvas;


    public void IsLogedStartGame()
    {
        SceneManager.LoadScene(2);
    }
    public void DesactivateCanvasMainMenu()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        logicAccCanvas.gameObject.SetActive(true);
    }
    public void ActivateCanvasMainMenu()
    {
        mainMenuCanvas.gameObject.SetActive(true);
        logicAccCanvas.gameObject.SetActive(false);
    }
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadCreateAccScene()
    {
        SceneManager.LoadScene(1);
    }
}
