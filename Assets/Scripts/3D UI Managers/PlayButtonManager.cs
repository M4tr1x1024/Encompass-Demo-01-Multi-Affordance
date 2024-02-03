using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayButtonManager : MonoBehaviour
{
    public TabletBeamInteractionManager localTabletBeamInteractionManager;
    public ForkBeamInteractionManager localForkBeamInteractionManager;
    public TextMeshProUGUI debugText;

    public MyEvent OnHoverEnter;
    public MyEvent OnHoverExit;
    public MyEvent OnFingerDown;
    public MyEvent OnFingerUp;

    public MyEvent OnMoviePause;
    public MyEvent OnMoviePlay;

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

                    /*if (videoInteractionState == false)
                    {
                        videoInteractionState = true;
                    }
                    else
                    {
                        videoInteractionState = false;
                    }
*/
                }
                else if (localTabletBeamInteractionManager.isDistanceClose == false && localTabletBeamInteractionManager.isDistanceClose != previousFingerState)
                {
                    // Finger Up
                    OnFingerUp.Invoke();

                    if (videoInteractionState == false)
                    {
                        moviePlayer.Play();
                        OnMoviePlay.Invoke();
                        videoInteractionState = true;
                    }
                    else
                    {
                        moviePlayer.Pause();
                        OnMoviePause.Invoke();
                        videoInteractionState = false;
                    }

                    // Log Debug Text
                    if (debugText != null)
                    {
                        debugText.text = "";
                    }
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

                    /*if (videoInteractionState == false)
                    {
                        videoInteractionState = true;
                    }
                    else
                    {
                        videoInteractionState = false;
                    }*/

                }
                else if (localForkBeamInteractionManager.isDistanceClose == false && localForkBeamInteractionManager.isDistanceClose != previousFingerState)
                {
                    // Finger Up
                    OnFingerUp.Invoke();

                    if (videoInteractionState == false)
                    {
                        moviePlayer.Play();
                        OnMoviePlay.Invoke();
                        videoInteractionState = true;
                    }
                    else
                    {
                        moviePlayer.Pause();
                        OnMoviePause.Invoke();
                        videoInteractionState = false;
                    }

                    // Log Debug Text
                    if (debugText != null)
                    {
                        debugText.text = "";
                    }
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
