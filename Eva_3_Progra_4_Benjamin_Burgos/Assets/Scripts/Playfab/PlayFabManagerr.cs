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

    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest { Email = loginEmailInput.text, Password = loginPasswordInput.text };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginsuccess, OnError);
    }
    void OnLoginsuccess(LoginResult result)
    {
        loginMessageText.text = "Logged in!";
    }


    public void ResetPasswordButton()
    {

    }


    void OnError(PlayFabError msg)
    {
        print(msg.ErrorMessage);
        registerMessageText.text = msg.ErrorMessage;
    }
}
