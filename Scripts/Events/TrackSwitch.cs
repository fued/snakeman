using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSwitch : MonoBehaviour
{
    [SerializeField]
    AudioManager.TrackList _track = AudioManager.TrackList.Silence;

    public void SwitchTrack()
    {
        AudioManager.Get().PlayTrack(_track);
    }
}
