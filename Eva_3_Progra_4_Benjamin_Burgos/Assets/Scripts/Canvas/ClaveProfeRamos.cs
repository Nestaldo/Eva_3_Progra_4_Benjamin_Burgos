using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClaveProfeRamos : MonoBehaviour
{
    [SerializeField] TMP_Text feedbackText;
    [SerializeField] TMP_InputField inputfieldClave;
    [SerializeField] string claveProfe;
    [SerializeField] SceneLoaderManager sceneLoaderManager;
    public void VerifyPassword()
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
}
