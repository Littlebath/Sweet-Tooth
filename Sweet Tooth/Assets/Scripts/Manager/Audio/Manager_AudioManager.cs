using UnityEngine.Audio;
using UnityEngine;
using System;

public class Manager_AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static Manager_AudioManager instance;

	// Use this for initialization
	void Awake ()
    {
        //DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
           s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixerGroup;
        }
	}

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if (s.source.isPlaying)
        {
            //Continue playing
            Debug.Log("Sound is already playing");
        }

        else if (!s.source.isPlaying)
        {
            StopAllAudio();
            s.source.Play();
            //Debug.Log("Sound is playing");
        }
    }

    public void StopAllAudio()
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }

    }
}
