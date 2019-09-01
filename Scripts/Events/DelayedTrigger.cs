using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayedTrigger : MonoBehaviour
{
    [SerializeField]
    private float _delay = 1f;

    [SerializeField]
    private bool _startOnAwake = false;

    public UnityEvent triggered;

    public void Awake()
    {
        if (_startOnAwake)
        {
            StartDelay();
        }
    }

    public void StartDelay()
    {
        Invoke("Delayed", _delay);
    }

    private void Delayed()
    {
        triggered.Invoke();
    }
}
