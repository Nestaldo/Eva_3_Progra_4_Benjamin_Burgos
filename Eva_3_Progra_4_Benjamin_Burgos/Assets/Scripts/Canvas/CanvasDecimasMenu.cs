using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class CanvasDecimasMenu : MonoBehaviour
{
    [SerializeField] GameObject VerticalGridLayout;

    [SerializeField] GameObject ramo1;
    [SerializeField] GameObject ramo2;
    [SerializeField] GameObject ramo3;
    [SerializeField] GameObject claveProfe;

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
        claveProfe.SetActive(false);
    }
    public void ActivateRamo1()
    {
        ramo1.SetActive(true);
        DesactivateVerticalGridLayout();
        ramo2.SetActive(false);
        ramo3.SetActive(false);
        claveProfe.SetActive(false);
        LoginPlayfabManager.Instance.SelectRamo("Ramo1");
    }
    public void ActivateRamo2()
    {
        ramo2.SetActive(true);
        DesactivateVerticalGridLayout();
        ramo1.SetActive(false);
        ramo3.SetActive(false);
        claveProfe.SetActive(false);
        LoginPlayfabManager.Instance.SelectRamo("Ramo2");
    }
    public void ActivateRamo3()
    {
        ramo3 .SetActive(true);
        DesactivateVerticalGridLayout();
        ramo1 .SetActive(false);
        ramo2 .SetActive(false);
        claveProfe.SetActive(false);
        LoginPlayfabManager.Instance.SelectRamo("Ramo3");
    }
    public void ActivatePassProfe()
    {
        claveProfe.SetActive(true);
        ramo3.SetActive(false);
        ramo1.SetActive(false);
        ramo2.SetActive(false);
        DesactivateVerticalGridLayout();
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
    //public void UseDecims()
    //{
    //    {
    //        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), result =>
    //        {
    //            if (result.Data != null && result.Data.ContainsKey("playerData"))
    //            {
    //                string playerDataJson = result.Data["playerData"].Value;
    //                PlayerData playerData = JsonUtility.FromJson<PlayerData>(playerDataJson);

    //                if (playerData != null)
    //                {
    //                    string currentRamo = LoginPlayfabManager.Instance.GetRamoSelected();
    //                    bool decimasRestadas = false;
    //                    switch (currentRamo)
    //                    {
    //                        case "Ramo1":
    //                            if (playerData.decimasRamo1 > 0)
    //                            {
    //                                playerData.decimasRamo1 = 0;
    //                                decimasRestadas = true;
    //                            }
    //                            break;
    //                        case "Ramo2":
    //                            if (playerData.decimasRamo2 > 0)
    //                            {
    //                                playerData.decimasRamo2 = 0;
    //                                decimasRestadas = true;
    //                            }
    //                            break;
    //                        case "Ramo3":
    //                            if (playerData.decimasRamo3 > 0)
    //                            {
    //                                playerData.decimasRamo3 = 0;
    //                                decimasRestadas = true;
    //                            }
    //                            break;
    //                        default:
    //                            Debug.LogWarning("Ramo no válido o no seleccionado.");
    //                            break;
    //                    }
    //                    if (decimasRestadas)
    //                    {
    //                        string updatedJson = JsonUtility.ToJson(playerData);
    //                        UpdateDecimasOnPlayFab(updatedJson);
    //                        UpdateDecimasText(0);
    //                        Debug.Log("Décimas utilizadas y guardadas.");
    //                    }
    //                    else
    //                    {
    //                        Debug.LogWarning("No hay décimas disponibles para restar.");
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                Debug.LogWarning("No se encontraron datos de jugador en PlayFab.");
    //            }
    //        }, OnError);
    //    }
    //}
    //private void UpdateDecimasOnPlayFab(string updateJson)
    //{
    //    PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
    //    {
    //        Data = new Dictionary<string, string> { { "playerData", updateJson } }
    //    },
    //     result => Debug.Log("Décimas actualizadas en PlayFab."),
    //     error => Debug.LogError("Error al actualizar las décimas en PlayFab: " + error.GenerateErrorReport()));
    //}
    public void FetchDecimasFromPlayfab()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataReceived, OnError);
        Debug.Log("Fetched Decimas");
    }

    void OnDataReceived(GetUserDataResult result)
    {
        if(result.Data != null && result.Data.ContainsKey("playerData"))
        {
            string playerDataJson = result.Data["playerData"].Value;
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(playerDataJson);
            if (playerData != null)
            {
                UpdateDecimasText(playerData.decimasRamo1);
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
        decimasText.text = ("Decimas Disponibles : " + decimas.ToString());
    }
}
