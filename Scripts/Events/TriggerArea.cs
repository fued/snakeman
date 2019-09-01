using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class TriggerArea : MonoBehaviour
{
    public UnityEvent triggered;

    [SerializeField]
    private string _targetTag = "Player";

    private bool _isActive = false;

    [SerializeField]
    private bool _oneshot = false;

    [SerializeField]
    private bool _canTriggerOnStart = false;

    public void Start()
    {
        Invoke("Startup", 0.1f);
    }

    private void Startup()
    {
        if (ObjectState.ObjectStates.Dead != StatefulObject.GetState(gameObject).State)
        {
            _isActive = true;

            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            collider.isTrigger = true;
            foreach (var obj in Physics2D.OverlapBoxAll(transform.position, collider.size, 0f))
            {
                if (obj.CompareTag(_targetTag))
                {
                    _isActive = false;
                    if (_canTriggerOnStart)
                    {
                        triggered.Invoke();
                    }
                }
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_isActive) && collision.CompareTag(_targetTag))
        {
            triggered.Invoke();
            _isActive = false;
            if (_oneshot)
            {
                BoxCollider2D collider = GetComponent<BoxCollider2D>();
                collider.enabled = false;
                StatefulObject.GetState(gameObject).State = ObjectState.ObjectStates.Dead;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(_targetTag))
        {
            _isActive = true;
        }
    }
}
