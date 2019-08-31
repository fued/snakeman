using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance = null;

    public float bpm = 140.0f;
    public int numBeatsPerSegment = 16;
    public List<AudioClip> clips = new List<AudioClip>();

    private double nextEventTime;
    private int flip = 0;
    private AudioSource[] audioSources = new AudioSource[2];
    private bool running = false;

    private float _musicVolume = 1f;

    public static AudioManager Get()
    {
        return _instance;
    }

    public void PlayTrack(int idx)
    {
        nextEventTime = AudioSettings.dspTime + 2.0f;

        audioSources[1 - flip].clip = clips[idx];
        audioSources[1 - flip].PlayScheduled(nextEventTime);
    }

    void Start()
    {
        if (null == _instance)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        for (int i = 0; i < 2; i++)
        {
            GameObject child = new GameObject("Player");
            child.transform.parent = gameObject.transform;
            audioSources[i] = child.AddComponent<AudioSource>();
        }

        PlayTrack(0);
        running = true;
    }

    void Update()
    {
        if (!running)
        {
            return;
        }

        audioSources[flip].volume = _musicVolume;

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
}
