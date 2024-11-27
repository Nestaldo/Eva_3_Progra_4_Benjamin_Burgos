using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClaveProfeRamos : MonoBehaviour
{
    [Header("Tmp Cavas")]
    [SerializeField] TMP_Text feedbackText;
    [SerializeField] TMP_InputField inputfieldClave;
    [Header("Claves Ramos")]
    [SerializeField] string claveProfe;
    [SerializeField] string claveProfe2;
    [SerializeField] string claveProfe3;
    [Header("SceneLoader")]
    [SerializeField] SceneLoaderManager sceneLoaderManager;
    public void VerifyPasswordRamo1()
    {
        if(inputfieldClave.text == claveProfe)
        {
            sceneLoaderManager.IsLogedStartGame();
        }
        else
        {
            feedbackText.text = "Wrong Password";
        }
    }
    public void VerifyPasswordRamo2()
    {
        if (inputfieldClave.text == claveProfe2)
        {
            sceneLoaderManager.IsLogedStartGame();
        }
        else
        {
            feedbackText.text = "Wrong Password";
        }
    }
    public void VerifyPasswordRamo3()
    {
        if (inputfieldClave.text == claveProfe3)
        {
            sceneLoaderManager.IsLogedStartGame();
        }
        else
        {
            feedbackText.text = "Wrong Password";
        }
    }
}
