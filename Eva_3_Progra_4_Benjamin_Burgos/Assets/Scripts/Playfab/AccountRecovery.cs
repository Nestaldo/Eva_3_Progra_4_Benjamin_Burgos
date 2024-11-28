using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AccountRecovery : MonoBehaviour
{
    [SerializeField] TMP_InputField emailAddres;
    [SerializeField] GameObject PanelPaQueNoToquenNa;
    [SerializeField] TMP_Text feedbackText;

    private void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "TU_TITTLE_ID";
        }
    }
    public void RecoverAccount()
    {
        Debug.Log($"PlayFab TitleId: {PlayFabSettings.TitleId}");
        var request = new SendAccountRecoveryEmailRequest { Email = emailAddres.text, TitleId = PlayFabSettings.TitleId };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRecoverySucces, OnError);
        PanelPaQueNoToquenNa.SetActive(true);
        Debug.Log("Realizada la solicitud para recuperacion de cuenta");
    }
    private void OnRecoverySucces(SendAccountRecoveryEmailResult result)
    {
        feedbackText.text = "Correo de recuperación enviado correctamente" ;
        PanelPaQueNoToquenNa.SetActive(false);
        Debug.Log(result);
    }
    void OnError(PlayFabError msg)
    {
        Debug.Log(msg);
        PanelPaQueNoToquenNa.SetActive(false);
        feedbackText.text = "Hubo un problema al enviar el correo, verifica que el correo esta correcto y tienes una cuenta";
    }
}
