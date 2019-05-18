using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using System;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public Sound[] songs;

    public static AudioManager instance;
    //public AudioSource masterAudioSource;
    public string currentSong;

    public Sound[] successSounds;
    public Sound[] sounds;
    //public Sound[] songStems;

    //public List<Sound> stemQueue;


    private void Awake()
    {
        //stemQueue = new List<Sound>();
        currentSong = "Song1";
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in songs) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (Sound s in successSounds)
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
        Double startTime = AudioSettings.dspTime + 1;
        foreach (Sound s in songs)
        {
            s.source.PlayScheduled(startTime);
        }
    }

    public void PlaySuccess()
    {
        Sound s = successSounds[UnityEngine.Random.Range(0, successSounds.Length)];
        Debug.Log(s.name);
        Play(successSounds, s.name);

    }

    public void ResetMusic()
    {
        Debug.Log("PRERESET: " + currentSong);
        ChangeMusic("Song1");
        Debug.Log("POSTRESET: " + currentSong);
    }

    public void ChangeMusic(string name)
    {
        currentSong = name;

        foreach (Sound s in songs)
        {
            if (s.name == name)
            {
                s.source.volume = 1;
            }
            else
            {
                s.source.volume = 0;
            }
        }
    }


    private void Play(Sound[] collection, string soundName)
    {
        Sound s = Array.Find(collection, sound => sound.name == soundName);
        s.source.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        Debug.Log("Sound: " + s.name + " with pitch: " + s.source.pitch);
        s.source.Play();
    }

    public void PlaySound (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        s.source.Play();
    }

}
