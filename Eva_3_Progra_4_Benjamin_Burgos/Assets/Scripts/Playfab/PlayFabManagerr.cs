using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;

public class PlayFabManagerr : MonoBehaviour
{
    [Header("Register UI")]
    public TextMeshProUGUI registerMessageText;
    public TMP_InputField registerEmailInput;
    public TMP_InputField RegisterPasswordInput;
    public TMP_InputField registerRepeatPasswordInput;

    private void Start()
    {
        if(toggle == true)
        {
            Autologin();
            InvokeRepeating(nameof(KeepSessionAlive), 0, 900);
        }
    }
    public void RegisterButton()
    {
        if(RegisterPasswordInput.text.Length < 6)
        {
            registerMessageText.text = "Pass is too shoort!, write at least 6 caracters";
            return;
        }
        if(RegisterPasswordInput.text != registerRepeatPasswordInput.text)
        {
            registerMessageText.text = "Passwords do not match!";
            return;
        }
        var request = new RegisterPlayFabUserRequest { Email = registerEmailInput.text, Password = RegisterPasswordInput.text, RequireBothUsernameAndEmail = false };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSucess, OnError);
    }
    void OnRegisterSucess(RegisterPlayFabUserResult result)
    {
        registerMessageText.text = "Success Register!";
    }


    [Header("Login UI")]
    public TextMeshProUGUI loginMessageText;
    public TMP_InputField loginEmailInput;
    public TMP_InputField loginPasswordInput;
    private string playfabId;
    private string sessionTicket;
    public Toggle toggle;

    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest { Email = loginEmailInput.text, Password = loginPasswordInput.text };
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
        registerMessageText.text = msg.ErrorMessage;
    }
    void Autologin()
    {
        string customId = PlayerPrefs.GetString("CustomID", null);
        if(string.IsNullOrEmpty(customId))
        {
            customId = System.Guid.NewGuid().ToString();
            PlayerPrefs.SetString("CustomID",customId);
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
