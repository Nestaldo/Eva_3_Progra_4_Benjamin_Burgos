using PlayFab.ClientModels;
using PlayFab;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginPlayfabManager : MonoBehaviour
{
    [Header("Login UI")]
    public TextMeshProUGUI loginMessageText;
    public TMP_InputField loginUserInput;
    public TMP_InputField loginPasswordInput;

    private string playfabId;
    private string sessionTicket;
    public Toggle toggle;

    private void Start()
    {
        if(toggle == true)
        {
            Autologin();
            InvokeRepeating(nameof(KeepSessionAlive), 0, 900);
        }
    }
    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest { Email = loginUserInput.text, Password = loginPasswordInput.text };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginsuccess, OnError);
    }
    void OnLoginsuccess(LoginResult result)
    {
        loginMessageText.text = "Logged in!";
        playfabId = result.PlayFabId;
        sessionTicket = result.SessionTicket;
    }
    void OnError(PlayFabError msg)
    {
        print(msg.ErrorMessage);
        loginMessageText.text = msg.ErrorMessage;
    }

    void Autologin()
    {
        string customId = PlayerPrefs.GetString("CustomID", null);
        if (string.IsNullOrEmpty(customId))
        {
            customId = System.Guid.NewGuid().ToString();
            PlayerPrefs.SetString("CustomID", customId);
        }
        var requestt = new LoginWithCustomIDRequest { CustomId = customId, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(requestt, OnLoginsuccess, OnError);
    }
    void KeepSessionAlive()
    {
        PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest(),
            result => Debug.Log("Sesion Activa"),
            error => Debug.LogError("Error al mantener la sesion: " + error.GenerateErrorReport()));
    }
    void OnSessionExpired()
    {
        Debug.Log("La sesion ha expirado, intentando reconectar...");
        Autologin();
    }
}
