using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSwitch : MonoBehaviour
{
    [SerializeField]
    AudioManager.AudioTracks _track = AudioManager.AudioTracks.Silence;

    public void SwitchTrack()
    {
        AudioManager.Get().PlayTrack(_track);
    }
}
