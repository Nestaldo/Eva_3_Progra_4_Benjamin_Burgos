using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Necesitamos esta dos librerias
using PlayFab;
using PlayFab.ClientModels;


public class PlayfabManagerGato : MonoBehaviour
{
    public string mail;
    public string pass;
    public string userName;
    private void Start()
    {
        //RegisteUser(mail, pass);
        LoginUser(mail, pass);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ChangeUserName(userName);
        }
    }

    public void RegisteUser(string mail, string pass)
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = mail,
            Password = pass,
            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterUserResult, OnError);
    }

    void OnRegisterUserResult(RegisterPlayFabUserResult result)
    {
        print("Usuario agregado");
    }

    public void LoginUser(string mail, string pass)
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = mail,
            Password = pass,
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginResult, OnError);
    }

    void OnLoginResult(LoginResult result)
    {
        print("Usuario logiao");
    }

    public void ChangeUserName(string userName)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = userName
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnChangeUserNameResult, OnError);
    }

    void OnChangeUserNameResult(UpdateUserTitleDisplayNameResult result)
    {
        print("Cambiao por " + result.DisplayName);
    }

    void OnError(PlayFabError msg)
    {
        print(msg.ErrorMessage);
    }
}
