using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVScreenManager : MonoBehaviour
{

    public TabletBeamInteractionManager localTabletBeamInteractionManager;
    public ForkBeamInteractionManager localForkBeamInteractionManager;
    //public GameObject remoteMovieSelectManager;
    public GameObject posters;
    public GameObject movie1;
    public GameObject uiBack;
    public GameObject tabletUIBlock;
    public GameObject forkUIBlock;
    public ConnectDetector tabletConnectDetector;
    public ConnectDetector forkConnectDetector;

    public string tvState = "menu";

    private Vector2 posterSizePressed = new Vector2(2.7f / 2f, 4.8f / 2f);
    private Vector2 posterSizeDefault = new Vector2(2.7f / 1.2f, 4.8f / 1.2f);
    private Vector2 posterSizeHover = new Vector2(2.7f, 4.8f);

    public RectTransform Poster1;
    public RectTransform Poster2;
    public RectTransform Poster3;


    // Start is called before the first frame update
    void Start()
    {
        tvState = "menu";

        //Poster size init
        Poster1.sizeDelta = posterSizeDefault;
        Poster2.sizeDelta = posterSizeDefault;
        Poster3.sizeDelta = posterSizeDefault;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (localTabletBeamInteractionManager.pressedUI == "poster1" || localTabletBeamInteractionManager.pressedUI == "poster2" || localTabletBeamInteractionManager.pressedUI == "poster3" || localForkBeamInteractionManager.pressedUI == "poster1" || localForkBeamInteractionManager.pressedUI == "poster2" || localForkBeamInteractionManager.pressedUI == "poster3")
        {
            //Parse TV state to connectDetector.cs
            tvState = "playing";

            localTabletBeamInteractionManager.pressedUI = "";
            localForkBeamInteractionManager.pressedUI = "";
            if (tabletConnectDetector.tvConnection)
            {
                tabletUIBlock.SetActive(true);
            }
            else
            {
                forkUIBlock.SetActive(true);
            }
            posters.SetActive(false);
            movie1.SetActive(true);
            //uiBack.SetActive(true);
        }
/*            else if (localTabletBeamInteractionManager.pressedUI == "uiBack")
        {
            //Parse TV state to connectDetector.cs
            tvState = "menu";

            localTabletBeamInteractionManager.pressedUI = "";
            tabletUIBlock.SetActive(false);
            forkUIBlock.SetActive(false);
            posters.SetActive(true);
            movie1.SetActive(false);
            uiBack.SetActive(false);
        }*/
    }
}
