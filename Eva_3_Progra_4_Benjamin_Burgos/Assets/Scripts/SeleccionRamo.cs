using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleccionRamo : MonoBehaviour
{
    public void ButtonRamo1(string nombreRamo)
    {
        LoginPlayfabManager.Instance.SelectRamo(nombreRamo);
    }

    void ParaObtenerlo()
    {
        string ramoSeleccionado = LoginPlayfabManager.Instance.GetRamoSelected();
    }
}
