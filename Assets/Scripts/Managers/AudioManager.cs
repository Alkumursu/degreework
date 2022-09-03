using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("BGM");
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void PlayOneShot(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.PlayOneShot(s.clip, s.volume);
    }

    public void StopPlaying(String sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + "Not Found!");
            return;

        }
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;

        s.source.Stop();
    }

    /*
    public void Fadeout(string musicname)
    {
        Sound s = Array.Find(sounds, sound => sound.name == musicname);

        IEnumerator fadeout()
        {
            AudioSource fadingmusic = s.source;

            while (fadingmusic.volume > 0)
            {
                fadingmusic.volume -= 0.005f;
                yield return null;
                //fadingmusic.Stop();
            }
        }

        StartCoroutine(fadeout());
    }

    public void Fadein(string musicname)
    {
        Sound s = Array.Find(sounds, sound => sound.name == musicname);

        IEnumerator fadein()
        {
            s.source.Play();
            float volume = 0f;

            do
            {
                s.source.volume = volume;
                volume += 0.05f;
                yield return null;
            } while (s.source.volume <= s.volume);

        }

        StartCoroutine(fadein());
    }*/
}
