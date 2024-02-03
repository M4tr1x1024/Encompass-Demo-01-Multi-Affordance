using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConnectDetector : MonoBehaviour
{
    public bool tvConnection = false;
    public TVScreenManager localTVScreenManager;
    //public TabletBeamInteractionManager localTabletBeamInteractionManager;
    //public ForkBeamInteractionManager localForkBeamInteractionManager;
    public DistanceDetector localTabletHandDistanceDetector;
    public DistanceDetector localForkHandDistanceDetector;
    public DistanceDetector localTabletHeadDistanceDetector;
    public DistanceDetector localForkHeadDistanceDetector;
    public ConnectDetector localTabletConnectDetector;
    public ConnectDetector localForkConnectDetector;

    public Outline tabletMesh;
    public Outline forkMesh;

    public GameObject tabletBeam;
    public GameObject tabletUIBlock;
    public GameObject forkBeam;
    public GameObject forkUIBlock;

    private DistanceDetector activeHandDistanceDetector;
    private DistanceDetector activeHeadDistanceDetector;
    private ConnectDetector inactiveConnectDetector;
    private Outline activeMesh;
    private GameObject activeBeam;
    private GameObject activeUIBlock;

    public Animator promptAnimator;

    public TextMeshProUGUI debugText;

    public float inactiveTimerStart;
    private float inactiveTimer = 0;

    public bool isTVConnecting = false;
    //public bool isTVConnected = false;
    public bool isTVDisconnecting = false;
    //public bool isTVDisconnected = false;

    //isTVInteractionActive is for detecting if the user is interacting with the TV or controller 3D UI
    public bool isTVInteractionActive = false;


    private void Start()
    {
        forkUIBlock.SetActive(false);
        tabletUIBlock.SetActive(false);

        //if current active controller is Fork
        if (localForkHandDistanceDetector != null && localForkHeadDistanceDetector != null)
        {
            activeHandDistanceDetector = localForkHandDistanceDetector;
            activeHeadDistanceDetector = localForkHeadDistanceDetector;
            inactiveConnectDetector = localTabletConnectDetector;
            activeMesh = forkMesh;
            activeBeam = forkBeam;
            activeUIBlock = forkUIBlock;
        }
        //if current active controller is Tablet
        else if (localTabletHandDistanceDetector != null && localTabletHeadDistanceDetector != null)
        {
            activeHandDistanceDetector = localTabletHandDistanceDetector;
            activeHeadDistanceDetector = localTabletHeadDistanceDetector;
            inactiveConnectDetector = localForkConnectDetector;
            activeMesh = tabletMesh;
            activeBeam = tabletBeam;
            activeUIBlock = tabletUIBlock;
        }
    }

    private void Update()
    {

        //Debug Text Log
        if (debugText != null)
        {
            debugText.text =
                "TV Connection: " + tvConnection.ToString() + System.Environment.NewLine +
                "TV Interaction Active: " + isTVInteractionActive.ToString() + System.Environment.NewLine +
                "TV State: " + localTVScreenManager.tvState + System.Environment.NewLine +
                "Inactive Timer: " + inactiveTimer.ToString() + System.Environment.NewLine +
                "Inactive Timer Start: " + inactiveTimerStart.ToString();
        }

        // if the user stops interacting with the TV or 3D UI
        if (activeHandDistanceDetector.isDistanceClose == false)
        {
            if (inactiveTimer > 0)
            {
                // if countdown timer is not 0, countdown timer starts
                inactiveTimer -= Time.deltaTime;
            }
            else
            {
                // countdown timer finished, disconnect.
                if (isTVConnecting == false && isTVDisconnecting == false && tvConnection == true)
                {
                    tvDisconnect();
                }
            }
        }
        // if the user is interacting with the TV or 3D UI again, reset the countdown timer
        else if (isTVInteractionActive == true)
        {
            inactiveTimer = inactiveTimerStart;
        }

    }

    // when controller beam collide with the "other"
    private void OnTriggerEnter(Collider other)
    {
        // reset inactive timer
        inactiveTimer = inactiveTimerStart;
        // if hand is close to tablet or fork & if the tablet or fork is close to head
        //if (activeHandDistanceDetector.isDistanceClose == true && activeHeadDistanceDetector.isDistanceClose == true)
        // temporarily removed head / controller distance detector
        if (activeHandDistanceDetector.isDistanceClose == true)
        {
            // if the controller beam is colliding with TV screen or controller 3D UI
            if (other.tag == "TV" || other.gameObject.layer == 6)
            {
                isTVInteractionActive = true;
                if (isTVConnecting == false && isTVDisconnecting == false && tvConnection == false)
                {
                    // activate Connection after 1 sec
                    Invoke("tvConnect", 1f);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "TV" || other.gameObject.layer == 6)
        {
            isTVInteractionActive = false;
        }
    }

    private void tvConnect()
    {
        inactiveConnectDetector.tvDisconnect();

        tvConnection = true;
        isTVInteractionActive = true;
        isTVConnecting = false;

        promptAnimator.Play("PromptAppear");

        activeMesh.OutlineWidth = 8;
        if (localTVScreenManager.tvState == "menu")
        {
            activeBeam.SetActive(true);
        }
        else if (localTVScreenManager.tvState == "playing")
        {
            activeBeam.SetActive(true);
            activeUIBlock.SetActive(true);
        }

    }

    private void tvDisconnect()
    {

        tvConnection = false;
        isTVInteractionActive = false;

        tabletBeam.SetActive(false);
        tabletUIBlock.SetActive(false);
        forkBeam.SetActive(false);
        forkUIBlock.SetActive(false);
        tabletMesh.OutlineWidth = 0;
        forkMesh.OutlineWidth = 0;
    }

    //private void tvInactive()
    //{
    //    isTVInteractionActive = false;
    //}
}