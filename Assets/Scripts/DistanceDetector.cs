using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceDetector : MonoBehaviour
{

    public GameObject tracker;
    public GameObject detector;
    public float threshold;
    public TextMeshProUGUI debugText;
    public bool isDistanceClose = false;

    private bool shouldOnDistanceCloseTrigger = false;
    private bool shouldOnDistanceFarTrigger = false;

    public MyEvent OnDistanceClose;
    public MyEvent OnDistanceFar;

    // Start is called before the first frame update
    //void Start()
    //{
    //}

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(tracker.transform.position, detector.transform.position);
        if (debugText != null)
        {
            double temp = dist - dist % 0.0001;
            debugText.text = temp.ToString();
        }

        if (dist <= threshold)
        {
            shouldOnDistanceFarTrigger = true;
            if (shouldOnDistanceCloseTrigger)
            {
                isDistanceClose = true;
                OnDistanceClose.Invoke();
                shouldOnDistanceCloseTrigger = false;
            }
        }
        else
        {
            shouldOnDistanceCloseTrigger = true;
            if (shouldOnDistanceFarTrigger)
            {
                isDistanceClose = false;
                OnDistanceFar.Invoke();
                shouldOnDistanceFarTrigger = false;
            }
        }
    }
}
