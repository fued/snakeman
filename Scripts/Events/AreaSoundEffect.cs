using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AreaSoundEffect : MonoBehaviour
{
    [SerializeField]
    private string _targetTag = "Player";

    [SerializeField]
    private AudioManager.SFX _sfx = AudioManager.SFX.River;

    private AudioSource _source = null;

    private float _sfxLength = 0f;

    private bool _keepPlaying = false;

    public void Start()
    {
        _sfxLength = AudioManager.Get().SfxLength(_sfx);
    }

    private IEnumerator InvokeRealtimeCoroutine(UnityAction action, float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        if (action != null)
        {
            action();
        }
    }

    private void PlayRepeat()
    {
        if (_keepPlaying)
        {
            _source = AudioManager.Get().PlaySfxOnce(_sfx);
            StartCoroutine(InvokeRealtimeCoroutine(PlayRepeat, _sfxLength));
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_targetTag))
        {
            _keepPlaying = true;
            StartCoroutine(InvokeRealtimeCoroutine(PlayRepeat, 0f));
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(_targetTag))
        {
            _keepPlaying = false;
            _source.Stop();
            Destroy(_source.gameObject);
        }
    }
}
