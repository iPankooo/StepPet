using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

//needed, because we call Obj from this class
[RequireComponent(typeof(ARRaycastManager))]

public class SpawnScript : MonoBehaviour
{


    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    private GameObject spawnedObj;
    public GameObject Petmodel;

    Animator dinoanim;

    
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
    }

    void Start()
    {

    }

    bool TryGetTouchPos(out Vector2 touchPosition)
    {

        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    private void Update()
    {
        

        //testing if user touches screen
        if (!TryGetTouchPos(out Vector2 touchPosition))
        {
            return;
        }

        //if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        if(Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                var touchPos = touch.position;
                bool isOverUI = touchPos.IsOverUIElement();

                if (isOverUI)
                {
                    Debug.Log("Raycast hits UI");
                }

                if (!isOverUI && raycastManager.Raycast(touchPosition, s_Hits, TrackableType.Planes))
                {
                    //first pos in list
                    var hitPose = s_Hits[0].pose;

                    if (spawnedObj == null)
                    {
                        spawnedObj = Instantiate(Petmodel, hitPose.position, hitPose.rotation) as GameObject;
                        dinoanim = spawnedObj.GetComponent<Animator>();
                    }
                    else
                    {
                        spawnedObj.transform.position = hitPose.position;
                        spawnedObj.transform.rotation = hitPose.rotation;
                    }

                }
            }
        }

   
    }

    public void Eat()
    {
        dinoanim.Play("Eating");
        StartCoroutine(ExecuteAfterTime(5));
    }
    public void Drink()
    {
        dinoanim.Play("Drinking");
        StartCoroutine(ExecuteAfterTime(5));
    }
    public void Jump()
    {
        dinoanim.Play("Jumping");
        StartCoroutine(ExecuteAfterTime(5));
    }


    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        dinoanim.Play("Idle");
    }
}
