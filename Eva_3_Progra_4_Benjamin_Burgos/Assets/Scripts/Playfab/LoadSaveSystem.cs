using PlayFab.ClientModels;
using PlayFab;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LoadSaveSystem
{
    Action<bool> onSaveEnd;

    public void SaveData<T>(T data, string dataKey, Action<bool> onEndSave)
    {
        string json = JsonUtility.ToJson(data);
        var requesst = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {dataKey, json }
            }
        };
        onSaveEnd = onEndSave;
        PlayFabClientAPI.UpdateUserData(requesst, onEndSaveData, OnError);
    }
    private void onEndSaveData(UpdateUserDataResult result)
    {
        onSaveEnd?.Invoke(true);
        onSaveEnd = null;
    }
    void OnError(PlayFabError msg)
    {
        Debug.Log(msg.ErrorMessage);

        onSaveEnd?.Invoke(false);
    }

    public void LoadData<T>(string dataKey, Action<T> onLoadData)
    {
        var request = new GetUserDataRequest();
        PlayFabClientAPI.GetUserData(request, result =>
        {
            if(result.Data != null && result.Data.ContainsKey(dataKey))
            {
                string json = result.Data[dataKey].Value;
                T data = JsonUtility.FromJson<T>(json);
                onLoadData(data);
            }
            else
            {
                onLoadData(default);
            }
        },OnError);
    }
}
