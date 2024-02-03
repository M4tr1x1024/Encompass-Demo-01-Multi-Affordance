using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTrigger : MonoBehaviour
{
    SecondTrigger secondTrigger;
    public GameObject getSecondTrigger;

    selectorManager LocalSelectorManager;
    public GameObject getSelectorManager;
    
    public bool isFirstTriggered;
    public bool canTrigger;
    public GameObject TestObject;
    

    private void Awake()
    {
        secondTrigger = getSecondTrigger.GetComponent<SecondTrigger>();
        LocalSelectorManager = getSelectorManager.GetComponent<selectorManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canTrigger)
        {
            isFirstTriggered = true;
            Invoke("stuckDetect", 1f);
            if (secondTrigger.isSecondTriggered && other.tag == "thumb")
            {
                // action
                Debug.Log("Up Swipe");
                isFirstTriggered = false;
                secondTrigger.isSecondTriggered = false;
                canTrigger = false;
                //TestObject.SetActive(true);
                if (LocalSelectorManager.selectedCount != 2)
                {
                    LocalSelectorManager.selectedCount += 1;
                }
                else
                {
                    LocalSelectorManager.selectedCount = 0;
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
        isFirstTriggered = false;
        secondTrigger.isSecondTriggered = false;
    }
}
