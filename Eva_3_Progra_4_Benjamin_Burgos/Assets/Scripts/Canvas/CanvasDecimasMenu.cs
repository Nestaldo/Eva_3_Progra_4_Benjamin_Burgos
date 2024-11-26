using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasDecimasMenu : MonoBehaviour
{
    [SerializeField] GameObject VerticalGridLayout;

    [SerializeField] GameObject ramo1;
    [SerializeField] GameObject ramo2;
    [SerializeField] GameObject ramo3;

    public void DesactivateVerticalGridLayout()
    {
        VerticalGridLayout.SetActive(false);
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
}
