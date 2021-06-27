using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHint : MonoBehaviour
{
    public GameObject DimmBackground;




    public void DimmDark()
    {
        DimmBackground.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    public void DimmLight()
    {
        DimmBackground.GetComponent<Image>().color = new Color32(0, 0, 0, 100);
    }
}
