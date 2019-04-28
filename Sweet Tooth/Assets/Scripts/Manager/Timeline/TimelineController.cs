using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public List<PlayableDirector> playableDirectors;

    public void Play ()
    {
        foreach (PlayableDirector playabledirector in playableDirectors)
        {
            playabledirector.Play();
        }
    }

    public void OnEnd ()
    {
        foreach (PlayableDirector playabledirector in playableDirectors)
        {
           
        }
    }

}
