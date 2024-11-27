using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

public class SesionIniciadaComo : MonoBehaviour
{
    [SerializeField] TMP_Text textoDeSesionIniciada;
    //[SerializeField] LoginPlayfabManager loginPlayfabManager;
    private void Start()
    {
        CheckLoginAndShowAccountName();
    }
    public void CheckLoginAndShowAccountName()
    {
        if (LoginPlayfabManager.instance != null && LoginPlayfabManager.instance.IsLoggedIn())
        {
            PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnSuccesGettingDataUser, OnError);
        }
        else
        {
            Debug.LogWarning("Sesion no iniciada o no encontrada");
        }
    }
    void OnSuccesGettingDataUser(GetAccountInfoResult result)
    {
        string email = result.AccountInfo.PrivateInfo.Email;
        textoDeSesionIniciada.text = $"Sesion iniciada como : {email}";
    }
    void OnError(PlayFabError msg)
    {
        Debug.Log($"Error retrieving data from PlayFab: {msg.GenerateErrorReport()}");
    }
}
