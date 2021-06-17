using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class InputManager : MonoBehaviour
{
    //[SerializeField] private GameObject arObj; 
    //we use the GameObj from the Datahandler
    [SerializeField] private Camera arCam;
    [SerializeField] private ARRaycastManager _raycastManager;
    //[SerializeField] allows us to add an object from the unity inspector

    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //equals a touch
        {
            Ray ray = arCam.ScreenPointToRay(Input.mousePosition);
            //Cam reads mouse/finger position and transforms it to a ray
            if (_raycastManager.Raycast(ray, _hits)) //looks if we hit anything
            {
                Pose pose = _hits[0].pose;
                //Instantiate(arObj, pose.position, pose.rotation);
                Instantiate(DataHandler.Instance.petmodel, pose.position, pose.rotation);
            }
        }
    }
}
//creates an ray and spawns an 3d model
