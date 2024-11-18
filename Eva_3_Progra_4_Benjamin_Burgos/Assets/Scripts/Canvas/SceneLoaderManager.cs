using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public void LoadLoginScene()
    {
        SceneManager.LoadScene(2);
    }
}
