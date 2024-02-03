using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectorManager : MonoBehaviour
{
    public int selectedCount = 0;
    public RectTransform Poster1;
    public RectTransform Poster2;
    public RectTransform Poster3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (selectedCount == 0)
        {
            Poster1.sizeDelta = new Vector2(2.7f, 4.8f);
            Poster2.sizeDelta = new Vector2(2.7f/1.2f, 4.8f/1.2f);
            Poster3.sizeDelta = new Vector2(2.7f / 1.2f, 4.8f / 1.2f);
        }
        else if (selectedCount == 1)
        {
            Poster1.sizeDelta = new Vector2(2.7f / 1.2f, 4.8f / 1.2f);
            Poster2.sizeDelta = new Vector2(2.7f, 4.8f);
            Poster3.sizeDelta = new Vector2(2.7f / 1.2f, 4.8f / 1.2f);
            /*            Canvas1.transform.localScale = new Vector3(0.1717807f, 0.1717807f, 0.1717807f);
                        Canvas2.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                        Canvas3.transform.localScale = new Vector3(0.1717807f, 0.1717807f, 0.1717807f);*/
        }
        else if (selectedCount == 2)
        {
            Poster1.sizeDelta = new Vector2(2.7f / 1.2f, 4.8f / 1.2f);
            Poster2.sizeDelta = new Vector2(2.7f / 1.2f, 4.8f / 1.2f);
            Poster3.sizeDelta = new Vector2(2.7f, 4.8f);
        }
    }
}
