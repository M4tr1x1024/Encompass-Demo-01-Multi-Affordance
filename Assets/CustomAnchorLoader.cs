using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAnchorLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpatialAnchorLoader>().LoadAnchorsByUuid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
