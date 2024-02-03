using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VolumeButtonManager : MonoBehaviour
{
    public TabletBeamInteractionManager localTabletBeamInteractionManager;
    public ForkBeamInteractionManager localForkBeamInteractionManager;
    public TextMeshProUGUI debugText;

    public TextMeshProUGUI volumeText;

    public MyEvent OnHoverEnter;
    public MyEvent OnHoverExit;
    public MyEvent OnFingerDown;
    public MyEvent OnFingerUp;

    public MyEvent onVolumeSettingActive;
    public MyEvent onVolumeSettingDeactive;
    public GameObject tablet;
    public GameObject fork;
    public UnityEngine.UI.Slider volumeSlider;

    private bool previousFingerState;

    private bool interactionState = false;
    public UnityEngine.Video.VideoPlayer moviePlayer;

    private Vector3 volumeStateLocation;
    private bool isVolumeSettingActive = false;
    private float adjustedVolume = 0f;
    private float currentVolume = 0f;

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
                    interactionState = false;
                }
                else if (localTabletBeamInteractionManager.isDistanceClose == false && localTabletBeamInteractionManager.isDistanceClose != previousFingerState)
                {
                    // Finger Up
                    OnFingerUp.Invoke();
                    if (interactionState == false)
                    {
                        interactionState = true;
                        if (isVolumeSettingActive == false)
                        {
                            onVolumeSettingActive.Invoke();
                            volumeStateLocation = tablet.transform.position;
                            currentVolume = moviePlayer.GetDirectAudioVolume(0);
                            volumeSlider.transform.position = new Vector3(volumeStateLocation.x, volumeStateLocation.y + 0.1f, volumeStateLocation.z + 0.18f - 0.36f * currentVolume);
                            volumeSlider.gameObject.SetActive(true);
                            isVolumeSettingActive = true;
                        }
                        else
                        {
                            onVolumeSettingDeactive.Invoke();
                            volumeSlider.gameObject.SetActive(false);
                            isVolumeSettingActive = false;
                        }
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

            //Volume Adjustment Update
            if (isVolumeSettingActive == true)
            {
                var volumeAdjustment = (tablet.transform.position.z - volumeStateLocation.z) * 400;
                adjustedVolume = (currentVolume * 100 + volumeAdjustment) - (currentVolume * 100 + volumeAdjustment) % 1;

                if (adjustedVolume >= 100)
                {
                    adjustedVolume = 100;
                }
                else if (adjustedVolume <= 0)
                {
                    adjustedVolume = 0;
                }

                moviePlayer.SetDirectAudioVolume(0, adjustedVolume / 100);
                volumeSlider.value = adjustedVolume / 100;
                // Log Volume Text
                volumeText.text = adjustedVolume.ToString();
            }
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
                    interactionState = false;

                }
                else if (localForkBeamInteractionManager.isDistanceClose == false && localForkBeamInteractionManager.isDistanceClose != previousFingerState)
                {
                    // Finger Up
                    OnFingerUp.Invoke();
                    if (interactionState == false)
                    {
                        interactionState = true;
                        if (isVolumeSettingActive == false)
                        {
                            onVolumeSettingActive.Invoke();
                            volumeStateLocation = fork.transform.position;
                            currentVolume = moviePlayer.GetDirectAudioVolume(0);
                            volumeSlider.transform.position = new Vector3(volumeStateLocation.x, volumeStateLocation.y + 0.1f, volumeStateLocation.z + 0.18f - 0.36f * currentVolume);
                            volumeSlider.gameObject.SetActive(true);
                            isVolumeSettingActive = true;
                        }
                        else
                        {
                            onVolumeSettingDeactive.Invoke();
                            volumeSlider.gameObject.SetActive(false);
                            isVolumeSettingActive = false;
                        }
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

            //Volume Adjustment Update
            if (isVolumeSettingActive == true)
            {
                var volumeAdjustment = (fork.transform.position.z - volumeStateLocation.z) * 400;
                adjustedVolume = (currentVolume * 100 + volumeAdjustment) - (currentVolume * 100 + volumeAdjustment) % 1;

                if (adjustedVolume >= 100)
                {
                    adjustedVolume = 100;
                }
                else if (adjustedVolume <= 0)
                {
                    adjustedVolume = 0;
                }

                moviePlayer.SetDirectAudioVolume(0, adjustedVolume / 100);
                volumeSlider.value = adjustedVolume / 100;
                // Log Volume Text
                volumeText.text = adjustedVolume.ToString();
            }
        }
    }
}
