using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PageSwipe : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 panelLocation;
    public float percentThreshold = 0.2f;
    public float easing = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        panelLocation = transform.position;
    }

    public void OnDrag(PointerEventData data)
    {
        //Debug.Log(data.pressPosition - data.position); we only need the x value
        //extracting our x value
        float difference = data.pressPosition.x - data.position.x;
        //subtracting the idle panel position with the position we drag
        transform.position = panelLocation - new Vector3(difference, 0, 0);
        Debug.Log(data.pressPosition - data.position);

    }

    public void OnEndDrag(PointerEventData data)
    {
        //Simple infinite swipe
        // panelLocation = transform.position; 

        //how much we have to swipe for a new screen
        // left -> right positive,  right -> left negative
        float percentage = (data.pressPosition.x - data.position.x) / Screen.width;

        if (Mathf.Abs(percentage) >= percentThreshold)
        {
            Vector3 newLocation = panelLocation;
            if (percentage > 0)
            {
                newLocation += new Vector3(-Screen.width, 0, 0);
            }
            else if (percentage < 0)
            {
                newLocation += new Vector3(Screen.width, 0, 0);
            }

            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            panelLocation = newLocation;
        }
        else
        {
            StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
        }

    }
    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f,1f,t));
            yield return 0;
        }
    }
}
