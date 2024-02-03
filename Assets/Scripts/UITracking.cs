using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITracking : MonoBehaviour
{

    public GameObject positionTrackingObject;
    public GameObject rotationTrackingObject;
    public GameObject mappingObject;
    public TextMeshProUGUI debugText;

    public float xOffset;
    public float yOffset;
    public float zOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mappingObject.transform.position = new Vector3(positionTrackingObject.transform.position.x + xOffset, positionTrackingObject.transform.position.y + yOffset, positionTrackingObject.transform.position.z + zOffset);

        //Trying to mapp rotation angle of the head but failed...
        //mappingObject.transform.eulerAngles = new Vector3(mappingObject.transform.eulerAngles.x, rotationTrackingObject.transform.eulerAngles.y + 90, mappingObject.transform.eulerAngles.z);

        if (debugText != null)
        {
            double tempX = positionTrackingObject.transform.rotation.x - positionTrackingObject.transform.rotation.x % 0.0001;
            double tempY = positionTrackingObject.transform.rotation.y - positionTrackingObject.transform.rotation.y % 0.0001;
            double tempZ = positionTrackingObject.transform.rotation.z - positionTrackingObject.transform.rotation.z % 0.0001;
            debugText.text = tempX.ToString() + "," + tempY.ToString() + "," + tempX.ToString();
        }
    }
}
