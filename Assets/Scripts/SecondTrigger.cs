using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondTrigger : MonoBehaviour
{
    FirstTrigger firstTrigger;
    public GameObject getFirstTrigger;

    selectorManager LocalSelectorManager;
    public GameObject getSelectorManager;

    public bool isSecondTriggered;
    public bool canTrigger;
    public GameObject TestObject;


    private void Awake()
    {
        firstTrigger = getFirstTrigger.GetComponent<FirstTrigger>();
        LocalSelectorManager = getSelectorManager.GetComponent<selectorManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canTrigger)
        {
            isSecondTriggered = true;
            Invoke("stuckDetect", 1f);
            if (firstTrigger.isFirstTriggered && other.tag == "thumb")
            {
                // action
                Debug.Log("Down Swipe");
                isSecondTriggered = false;
                firstTrigger.isFirstTriggered = false;
                canTrigger = false;
                //TestObject.SetActive(false);
                if (LocalSelectorManager.selectedCount != 0)
                {
                    LocalSelectorManager.selectedCount -= 1;
                }
                else
                {
                    LocalSelectorManager.selectedCount = 2;
                }
            }
            Invoke("canTriggerReset", 0.3f);
        }
    }

    private void canTriggerReset()
    {
        canTrigger = true;
    }

    private void stuckDetect()
    {
        firstTrigger.isFirstTriggered = false;
        isSecondTriggered = false;
    }
}
