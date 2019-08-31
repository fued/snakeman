using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionEffect : MonoBehaviour
{
    public void Start()
    {
        transform.SetAsLastSibling();
    }

    public void FadeOut()
    {
        GetComponent<Animator>().Play("TransitionOut");
    }
}
