 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonManager : MonoBehaviour
{
    private Button button;
    public GameObject petmodel;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SpawnObj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObj()
    {
        DataHandler.Instance.petmodel = petmodel; //gives the gamemodel to the ButtonManager
    }
}
