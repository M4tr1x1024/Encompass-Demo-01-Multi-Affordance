using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TabletBeamInteractionManager : MonoBehaviour
{
    public RectTransform Poster1;
    public RectTransform Poster2;
    public RectTransform Poster3;

    public GameObject tracker;
    public GameObject detector;
    public float threshold;
    public bool isDistanceClose = false;
    private bool isDistanceCloseTimerDone = false;

    public TextMeshProUGUI debugText;

    public bool shouldOnDistanceCloseTrigger = false;
    public bool shouldOnDistanceFarTrigger = false;
    private string hoveredUI;

    private Vector2 posterSizePressed = new Vector2(2.7f / 2f, 4.8f / 2f);
    private Vector2 posterSizeDefault = new Vector2(2.7f / 1.2f, 4.8f / 1.2f);
    private Vector2 posterSizeHover = new Vector2(2.7f, 4.8f);

    public string pressedUI = "";

    public bool triggerEntered = false;
    public bool triggerExited = false;
    public GameObject triggeredObject;

    private void Start()
    {

    }

    private void Update()
    {

        //Tracking distance between thumb and tablet pressing area -> Finger Down & Finger Up

        float dist = Vector3.Distance(tracker.transform.position, detector.transform.position);

        //Debug Text Log
        if (debugText != null)
        {
            debugText.text = dist.ToString();
        }

        if (dist <= threshold)
        {
            shouldOnDistanceFarTrigger = true;

            // Parse the selected poster data to external
            pressedUI = hoveredUI;

            if (shouldOnDistanceCloseTrigger)
            {
                isDistanceClose = true;
                Invoke("setIsDistanceCloseTimerDone", 0.3f);
                if (hoveredUI == "poster1")
                {
                    Poster1.sizeDelta = posterSizePressed;
                }
                else if (hoveredUI == "poster2")
                {
                    Poster2.sizeDelta = posterSizePressed;
                }
                else if (hoveredUI == "poster3")
                {
                    Poster3.sizeDelta = posterSizePressed;
                }
                else if (hoveredUI == "uiBack")
                {

                }
                shouldOnDistanceCloseTrigger = false;
            }
        }
        else
        {
            shouldOnDistanceCloseTrigger = true;
            if (shouldOnDistanceFarTrigger && isDistanceCloseTimerDone)
            {
                isDistanceClose = false;
                isDistanceCloseTimerDone = false;

                if (hoveredUI == "poster1")
                {
                    Poster1.sizeDelta = posterSizeHover;
                }
                else if (hoveredUI == "poster2")
                {
                    Poster2.sizeDelta = posterSizeHover;
                }
                else if (hoveredUI == "poster3")
                {
                    Poster3.sizeDelta = posterSizeHover;
                }
                shouldOnDistanceFarTrigger = false;

            }

            
        }
    }

    private void setIsDistanceCloseTimerDone()
    {
        isDistanceCloseTimerDone = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Detect beam hover in poster

        hoveredUI = other.tag;
        if (other.tag == "poster1")
        {
            Poster1.sizeDelta = posterSizeHover;
            Poster2.sizeDelta = posterSizeDefault;
            Poster3.sizeDelta = posterSizeDefault;
        }
        else if (other.tag == "poster2")
        {
            Poster1.sizeDelta = posterSizeDefault;
            Poster2.sizeDelta = posterSizeHover;
            Poster3.sizeDelta = posterSizeDefault;
        }
        else if (other.tag == "poster3")
        {
            Poster1.sizeDelta = posterSizeDefault;
            Poster2.sizeDelta = posterSizeDefault;
            Poster3.sizeDelta = posterSizeHover;
        }

        //Detect beam hover on 3D UI

        if (other.gameObject.layer == 6 || other.gameObject.layer == 5)
        {
            triggerEntered = true;
            triggerExited = false;
            triggeredObject = other.gameObject;
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        //Detect beam hover out poster

        if (!isDistanceClose)
        {
            hoveredUI = "";
            Poster1.sizeDelta = posterSizeDefault;
            Poster2.sizeDelta = posterSizeDefault;
            Poster3.sizeDelta = posterSizeDefault;
        }

        //Detect beam hover on 3D UI

        if (other.gameObject.layer == 6 || other.gameObject.layer == 5)
        {
            triggerExited = true;
            triggerEntered = false;
            triggeredObject = null;

            //Debug Text Log
            if (debugText != null)
            {
                debugText.text = "";
            }
        }
    }
}
