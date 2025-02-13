using PlayFab.ClientModels;
using PlayFab;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LoginPlayfabManager : MonoBehaviour
{
    [Header("Login UI")]
    public TextMeshProUGUI loginMessageText;
    public TMP_InputField loginUserInput;
    public TMP_InputField loginPasswordInput;

    [SerializeField] GameObject panelPaQueNoToqueNa;
    private string playfabId;
    private string sessionTicket;
    [SerializeField] SceneLoaderManager sceneLoaderManager;
    public Toggle toggle;
    public string currentRamo;

    public static LoginPlayfabManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public bool IsLoggedIn()
    {
        return PlayFabClientAPI.IsClientLoggedIn();
    }
    public void LoginButton()
    {
        if(loginUserInput.text != null && loginPasswordInput.text != null)
        {
            var request = new LoginWithEmailAddressRequest { Email = loginUserInput.text, Password = loginPasswordInput.text };
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginsuccess, OnError);
            panelPaQueNoToqueNa.SetActive(true);
        }
    }
    void OnLoginsuccess(LoginResult result)
    {
        loginMessageText.text = "Logged in!";
        playfabId = result.PlayFabId;
        sessionTicket = result.SessionTicket;
        sceneLoaderManager.LoadDecimasMenuScene();
        panelPaQueNoToqueNa.SetActive(false);
    }
    void OnError(PlayFabError msg)
    {
        print(msg.ErrorMessage);
        loginMessageText.text = msg.ErrorMessage;
        panelPaQueNoToqueNa.SetActive(false);
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
        sceneLoaderManager.IsLogedStartGame();
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

    public void SelectRamo(string ramo)
    {
        currentRamo = ramo;
    }

    public string GetRamoSelected()
    {
        return currentRamo;
    }    
}
