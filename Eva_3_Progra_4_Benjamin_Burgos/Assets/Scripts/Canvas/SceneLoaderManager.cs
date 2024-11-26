using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadCreateAccScene()
    {
        SceneManager.LoadScene(1);
    }
    public void IsLogedStartGame()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadDecimasMenuScene()
    {
        SceneManager.LoadScene(3);
    }
    public void LoadRecoveryAccountScene()
    {
        SceneManager.LoadScene(4);
    }
}
