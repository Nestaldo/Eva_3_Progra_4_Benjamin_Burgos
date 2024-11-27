using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEditor.PackageManager;

public class CanvasDecimasMenu : MonoBehaviour
{
    [SerializeField] GameObject VerticalGridLayout;

    [SerializeField] GameObject ramo1;
    [SerializeField] GameObject ramo2;
    [SerializeField] GameObject ramo3;

    [SerializeField] TMP_Text decimasText;

    [SerializeField] LoginPlayfabManager loginPlayfabManager;

    public void DesactivateVerticalGridLayout()
    {
        VerticalGridLayout.SetActive(false);
    }
    public void ReactivateVerticalGridLayout()
    {
        VerticalGridLayout.SetActive(true);
        ramo1.SetActive(false);
        ramo2.SetActive(false);
        ramo3.SetActive(false);
    }
    public void ActivateRamo1()
    {
        ramo1.SetActive(true);
        DesactivateVerticalGridLayout();
        ramo2.SetActive(false);
        ramo3.SetActive(false);
    }
    public void ActivateRamo2()
    {
        ramo2.SetActive(true);
        DesactivateVerticalGridLayout();
        ramo1.SetActive(false);
        ramo3.SetActive(false);
    }
    public void ActivateRamo3()
    {
        ramo3 .SetActive(true);
        DesactivateVerticalGridLayout();
        ramo1 .SetActive(false);
        ramo2 .SetActive(false);
    }
    public void CheckLoginAndFetchDecimas()
    {
        if(loginPlayfabManager!= null && loginPlayfabManager.IsLoggedIn())
        {
            FetchDecimasFromPlayfab();
        }
        else
        {
            Debug.LogWarning("Sesion no iniciada o no encontrada");
        }
    }
    public void FetchDecimasFromPlayfab()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataReceived, OnError);
    }

    void OnDataReceived(GetUserDataResult result)
    {
        if(result.Data != null && result.Data.ContainsKey("playerData"))
        {
            string playerDataJson = result.Data["playerData"].Value;
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(playerDataJson);
            if (playerData != null)
            {
                UpdateDecimasText(playerData.decimas);
            }
            else
            {
                Debug.LogWarning("No se encontraron las decimas");
            }
        }
        else
        {
            Debug.LogWarning("Key ´playerData´ not found in PlayFab user data");
        }
    }
    void OnError(PlayFabError msg)
    {
        Debug.Log($"Error retrieving data from PlayFab: {msg.GenerateErrorReport()}");
    }
    private void UpdateDecimasText(float decimas)
    {
        decimasText.text = decimas.ToString();
    }
}
