using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Audio_SnapManagement : MonoBehaviour
{

    public AudioMixerSnapshot playSnap;
    public AudioMixerSnapshot pauseSnap;

    Text soundDisplay;
    bool paused = false;

    // Use this for initialization
    void Start()
    {
        soundDisplay = gameObject.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPaused()
    {
        paused = !paused;

        if (paused)
        {
            pauseSnap.TransitionTo(0.3f);
            soundDisplay.text = "Sound: Off";
        }
        else
        {
            playSnap.TransitionTo(0.3f);
            soundDisplay.text = "Sound: On";
        }
    }
}
