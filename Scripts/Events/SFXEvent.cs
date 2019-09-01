using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXEvent : MonoBehaviour
{
    [SerializeField]
    AudioManager.SFX _sfx = AudioManager.SFX.Secret_Opening_Area;

    public void PlayEffect()
    {
        AudioManager.Get().PlaySfxOnce(_sfx);
    }
}
