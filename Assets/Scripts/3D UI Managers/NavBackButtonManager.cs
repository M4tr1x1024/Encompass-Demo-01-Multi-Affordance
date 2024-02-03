using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NavBackButtonManager : MonoBehaviour
{
    public TVScreenManager localTVScreenManager;
    public ConnectDetector localTabletConnectDetector;
    public ConnectDetector localForkConnectDetector;
    public TabletBeamInteractionManager localTabletBeamInteractionManager;
    public ForkBeamInteractionManager localForkBeamInteractionManager;
    public TextMeshProUGUI debugText;

    public MyEvent OnHoverEnter;
    public MyEvent OnHoverExit;
    public MyEvent OnFingerDown;
    public MyEvent OnFingerUp;

    private bool previousFingerState;

    private bool videoInteractionState = false;
    public UnityEngine.Video.VideoPlayer moviePlayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
        //Detect whether tablet or fork in use
        if (localTabletBeamInteractionManager != null)
        {
            if (localTabletBeamInteractionManager.triggerEntered == true && localTabletBeamInteractionManager.triggeredObject == transform.gameObject)
            {
                // Hover Entered
                OnHoverEnter.Invoke();

                if (localTabletBeamInteractionManager.isDistanceClose)
                {
                    // Finger Down
                    OnFingerDown.Invoke();
                    localTVScreenManager.tvState = "menu";
                    localTabletConnectDetector.isTVInteractionActive = false;
                    localForkConnectDetector.isTVInteractionActive = false;

                }
                else if (localTabletBeamInteractionManager.isDistanceClose == false && localTabletBeamInteractionManager.isDistanceClose != previousFingerState)
                {
                    // Finger Up
                    OnFingerUp.Invoke();
                }
            }
            else
            {
                // Hover Exited
                OnHoverExit.Invoke();
            }

            //Store Finger State
            previousFingerState = localTabletBeamInteractionManager.isDistanceClose;
        }
        else if (localForkBeamInteractionManager != null)
        {
            if (localForkBeamInteractionManager.triggerEntered == true && localForkBeamInteractionManager.triggeredObject == transform.gameObject)
            {
                // Hover Entered
                OnHoverEnter.Invoke();

                if (localForkBeamInteractionManager.isDistanceClose)
                {
                    // Finger Down
                    OnFingerDown.Invoke();
                    localTVScreenManager.tvState = "menu";
                    localTabletConnectDetector.isTVInteractionActive = false;
                    localForkConnectDetector.isTVInteractionActive = false;

                }
                else if (localForkBeamInteractionManager.isDistanceClose == false && localForkBeamInteractionManager.isDistanceClose != previousFingerState)
                {
                    // Finger Up
                    OnFingerUp.Invoke();
                }
            }
            else
            {
                // Hover Exited
                OnHoverExit.Invoke();
            }

            //Store Finger State
            previousFingerState = localForkBeamInteractionManager.isDistanceClose;
        }
        

    }
}
