using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HamMenuButtons : MonoBehaviour
{

    private Button button;
    
    // Start is called before the first frame update
    void Start()
    {
        //Making a button to Reset Data
        button = GetComponent<Button>();
        button.onClick.AddListener(ResetSave);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ResetSave()
    {
        PlayerPrefs.DeleteAll();
    }


}
