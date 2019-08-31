﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    public enum TrackList
    {
        Silence = 0,
        Jungle,
        Sample,
    }

    private static AudioManager _instance = null;

    public float bpm = 140.0f;
    public int numBeatsPerSegment = 16;
    public List<AudioClip> clips = new List<AudioClip>();

    private double nextEventTime;
    private int flip = 0;
    private AudioSource[] audioSources = new AudioSource[2];
    private bool running = false;

    private float _musicVolume = 1f;

    private bool _keepFadingIn = false;
    private bool _keepFadingOut = false;

    private float _fadeMultiplier = 1f;

    [SerializeField]
    private float _fadeTime = 2f;

    public static AudioManager Get()
    {
        if (null == _instance)
        {
            GameObject audioManagerPrefab = Resources.Load<GameObject>("Prefabs/AudioManager");
            GameObject audioManagerObject = Instantiate<GameObject>(audioManagerPrefab);

            _instance = audioManagerObject.GetComponent<AudioManager>();
            _instance.Setup();
            DontDestroyOnLoad(_instance);
        }

        return _instance;
    }

    private IEnumerator InvokeRealtimeCoroutine(UnityAction action, float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        if (action != null)
        {
            action();
        }
    }

    public void PlayTrack(TrackList idx)
    {
        nextEventTime = AudioSettings.dspTime + 2.0f;

        audioSources[1 - flip].clip = clips[(int)idx];
        audioSources[1 - flip].PlayScheduled(nextEventTime);

        StartFadeOut();
        StartCoroutine(InvokeRealtimeCoroutine(StartFadeIn, 2f));
    }

    void Start()
    {
        if (Get() != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Setup()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject child = new GameObject("AudioPlayer");
            child.transform.parent = gameObject.transform;
            audioSources[i] = child.AddComponent<AudioSource>();
        }

        PlayTrack(TrackList.Sample);
        running = true;
    }

    void Update()
    {
        if (!running)
        {
            return;
        }

        audioSources[flip].volume = _musicVolume * _fadeMultiplier;

        double time = AudioSettings.dspTime;

        if (time + 1.0f > nextEventTime)
        {
            // We are now approx. 1 second before the time at which the sound should play,
            // so we will schedule it now in order for the system to have enough time
            // to prepare the playback at the specified time. This may involve opening
            // buffering a streamed file and should therefore take any worst-case delay into account.
            audioSources[flip].clip = audioSources[1 - flip].clip;
            audioSources[flip].PlayScheduled(nextEventTime);

            // Place the next event 16 beats from here at a rate of 140 beats per minute
            nextEventTime += audioSources[flip].clip.length;

            // Flip between two audio sources so that the loading process of one does not interfere with the one that's playing out
            flip = 1 - flip;
        }
    }

    private void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        _keepFadingIn = true;
        _keepFadingOut = false;

        _fadeMultiplier = 0f;

        while (_keepFadingIn && (1f !=_fadeMultiplier))
        {
            _fadeMultiplier = Mathf.Min(1f, _fadeMultiplier + (0.1f / _fadeTime));
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

    private void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        _keepFadingIn = false;
        _keepFadingOut = true;

        _fadeMultiplier = 1f;

        while (_keepFadingOut && (0f != _fadeMultiplier))
        {
            _fadeMultiplier = Mathf.Max(0f, _fadeMultiplier - (0.1f / _fadeTime));
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}
