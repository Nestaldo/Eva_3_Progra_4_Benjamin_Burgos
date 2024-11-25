using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainMenu : MonoBehaviour
{

    public Canvas mainMenuCanvas;
    public Canvas logicAccCanvas;

    public void DesactivateCanvasMainMenu()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        logicAccCanvas.gameObject.SetActive(true);
    }
    public void ActivateCanvasMainMenu()
    {
        mainMenuCanvas.gameObject.SetActive(true);
        logicAccCanvas.gameObject.SetActive(false);
    }
}
