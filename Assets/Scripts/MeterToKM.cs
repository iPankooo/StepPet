using PedometerU.Tests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterToKM : MonoBehaviour
{
    private float loadMeter;
    private float loadKM;
    private float adderKM;
    private float adderMeter;
    private float distancef2;
    private float loadDistance2;
    public Text distanceKMText;

    // Start is called before the first frame update
    void Update()
    {
        adderMeter = GameObject.Find("UI_Statistics").GetComponent<StepTest>().adderD;
        loadMeter = GameObject.Find("UI_Statistics").GetComponent<StepTest>().loadDistance;
        distancef2 = GameObject.Find("UI_Statistics").GetComponent<StepTest>().distancef;
        loadDistance2 = GameObject.Find("UI_Statistics").GetComponent<StepTest>().loadDistance;


        adderKM = adderMeter / 1000;
        loadKM = loadMeter / 1000;

        
        if (loadDistance2 == distancef2)
        {
            distanceKMText.text = loadKM.ToString();

        }
        else
        {

            distanceKMText.text = adderKM.ToString();
        }



    }

   
    
}
