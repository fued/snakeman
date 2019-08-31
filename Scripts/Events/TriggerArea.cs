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

    private bool isActive = false;

    public void Start()
    {
        Invoke("Startup", 0.1f);
    }

    private void Startup()
    {
        isActive = true;

        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.isTrigger = true;
        foreach (var obj in Physics2D.OverlapBoxAll(transform.position, collider.size, 0f))
        {
            if (obj.CompareTag(_targetTag))
            {
                isActive = false;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if ((isActive) && collision.CompareTag(_targetTag))
        {
            triggered.Invoke();
            isActive = false;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(_targetTag))
        {
            isActive = true;
        }
    }
}
