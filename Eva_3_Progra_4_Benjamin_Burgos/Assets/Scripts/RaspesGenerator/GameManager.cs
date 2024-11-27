using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] RaspeGenerator raspeGenerator;
    [SerializeField] TMPro.TextMeshProUGUI textInstruction;
    [SerializeField] GameObject goMenuButton;

    CatStateType gameState = CatStateType.Instantiate;

    private void Start()
    {
        raspeGenerator.GenerateCats();

    }

    private void Update()
    {
        if(Cat.state != gameState)
        {
            gameState = Cat.state;
            ChangeState(gameState);
        }
    }

    private void ChangeState(CatStateType gameState)
    {
        switch(gameState)
        {
            case CatStateType.Instantiate:
                break;
            case CatStateType.Selection:
                textInstruction.text = "Escoje un gato y Clickealo";
                break;
            case CatStateType.Gaming:
                textInstruction.text = "Clickea el gato pra quitarle sus decimas!!";
                break;
            case CatStateType.Finish:
                textInstruction.text = "El juego ha terminado, Felicidades por tus decimas!!";
                goMenuButton.SetActive(true);
                break;
        }
    }

}
