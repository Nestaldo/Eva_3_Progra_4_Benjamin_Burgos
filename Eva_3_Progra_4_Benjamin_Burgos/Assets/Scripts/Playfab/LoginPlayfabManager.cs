using PlayFab.ClientModels;
using PlayFab;
using TMPro;
using UnityEngine;

public class LoginPlayfabManager : MonoBehaviour
{
    [Header("Login UI")]
    public TextMeshProUGUI loginMessageText;
    public TMP_InputField loginUserInput;
    public TMP_InputField loginPasswordInput;

    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest { Email = loginUserInput.text, Password = loginPasswordInput.text };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginsuccess, OnError);
    }
    void OnLoginsuccess(LoginResult result)
    {
        loginMessageText.text = "Logged in!";
    }
    void OnError(PlayFabError msg)
    {
        print(msg.ErrorMessage);
        loginMessageText.text = msg.ErrorMessage;
    }
}
