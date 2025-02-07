using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] CatAnimController catAnim;
    [SerializeField] RandomMoveRB movement;
    [SerializeField] GameObject selectFeedback;
    [SerializeField] PruebaCUbito2 pruebaCubito2;
    [SerializeField] CatVFX_SFX vfxSfx;

    [SerializeField] float life;

    public static CatStateType state = CatStateType.Instantiate;
    float value;
    float time;
    float timeToChange;
    bool isStartGaming;
    bool isGameEnded;
    bool isSelected = false;

    private void Start()
    {
        life = Random.Range(1, 10);
        vfxSfx.SimpleMow();
        catAnim.Jump(true);
    }

    private void Update()
    {
        if (state == CatStateType.Gaming)
        {
            if (!isStartGaming)
            {
                StartGaming();
            }

            CatTimer();
        }
        else if (state == CatStateType.Finish)
        {
            if (!isGameEnded)
            {
                EndGaming();
            }
        }
    }

    private void StartGaming()
    {
        catAnim.Jump(false);
        catAnim.Walk(true);
        isStartGaming = true;
        movement.isMoveEnable = true;
    }

    private void EndGaming()
    {
        if (life > 0)
        {
            catAnim.Reset();
            catAnim.Jump(true);
        }
    }

    private void CatTimer()
    {
        time += Time.deltaTime;
        if (time >= timeToChange)
        {
            timeToChange = Random.Range(.5f, .8f);
            time = 0;
            movement.RandomizeDirAndSpd();
        }
    }

    public void SetState(CatStateType stateType)
    {
        state = stateType;
    }

    public void SetCat(float val, AudioSource aSource)
    {
        value = val;
        catAnim.Jump(false);
        pruebaCubito2.audioSource = aSource;
    }

    void SelectCat()
    {
        state = CatStateType.Gaming;
        selectFeedback.SetActive(true);
        isSelected = true;
        pruebaCubito2.isSelecet = true;
        pruebaCubito2.indiceFrecuencia = 10;
        life = 9;
        //GetComponent<Rigidbody>().mass = 10;
    }

    void DoDamage()
    {
        life--;
        if (life <= 0)
        {
            Dead();
        }
        else
        {
            movement.RandomizeDirAndSpd();
            vfxSfx.Hit(isSelected);
        }
    }

    void Dead()
    {
        catAnim.Dead();
        vfxSfx.Dead();
        pruebaCubito2.audioSource = null;
        if (isSelected)
        {
            state = CatStateType.Finish;
            Invoke("ShowDecimas", 1);
        }
        else
        {
            movement.isMoveEnable = false;
        }
    }

    void ShowDecimas()
    {
        print(value);
        //aca termino el juego
        vfxSfx.ShowDecimas(value);

        LoadData();
    }

    void LoadData()
    {
        LoadSaveSystem loadDava = new LoadSaveSystem();
        loadDava.LoadData<PlayerData>("playerData", SaveData);
    }

    void SaveData(PlayerData playerData)
    {
        LoadSaveSystem LoadData = new LoadSaveSystem();
        PlayerData tempPlayerData = playerData;

        string ramoActivo = LoginPlayfabManager.Instance.currentRamo;
        switch (ramoActivo) 
        {
            case "Ramo1":
                tempPlayerData.decimasRamo1 += value;
                break;
            case "Ramo2":
                tempPlayerData.decimasRamo2 += value;
                break;
            case "Ramo3":
                tempPlayerData.decimasRamo3 += value;
                break;
            default:
                Debug.LogWarning("Ramo desconocido, selecciona un ramo valido");
                    return;
        }

        LoadData.SaveData(tempPlayerData, "playerData", OnEndSave);
    }
    void SaveDataToPlayfab(PlayerData playerData)
    {
        string playerDataJson = JsonUtility.ToJson(playerData);

        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> {
            { "playerData", playerDataJson }
        }
        },
        result => Debug.Log("Datos guardados en PlayFab correctamente."),
        error => Debug.LogError("Error al guardar en PlayFab: " + error.GenerateErrorReport()));

    }

    void OnEndSave(bool isSucces)
    {
        if(isSucces)
        {
            print("yeeeeiii");
            LoadSaveSystem loadDava = new LoadSaveSystem();
            loadDava.LoadData<PlayerData>("playerData", SaveDataToPlayfab);
        }
        else
        {
            print("Nohhh");
        }
    }

    private void OnMouseEnter()
    {
        if (state == CatStateType.Selection)
        {
            catAnim.Jump(true);
        }
    }

    private void OnMouseExit()
    {
        if (state == CatStateType.Selection)
        {
            catAnim.Jump(false);
        }
    }

    private void OnMouseDown()
    {
        if (state == CatStateType.Selection)
        {
            SelectCat();
        }
        else if(state == CatStateType.Gaming && life > 0)
        {
            DoDamage();
        }

    }

}
