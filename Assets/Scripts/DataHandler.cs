using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    public GameObject petmodel;

    //this is just for calling anything from here
    //we can use the instance property to call any method or property from this class

    public static DataHandler instance;
    public static DataHandler Instance 
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataHandler    >();
            }
            return instance;
        } 
    }
}
