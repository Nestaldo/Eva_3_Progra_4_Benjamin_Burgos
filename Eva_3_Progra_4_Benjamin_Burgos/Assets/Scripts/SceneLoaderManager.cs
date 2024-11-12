using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
    public void LoadCreateAccScene()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadLoginScene()
    {
        SceneManager.LoadScene(2);
    }
}
