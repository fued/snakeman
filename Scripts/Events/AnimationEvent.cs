using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : MonoBehaviour
{
    public UnityEvent animationDone;

    [SerializeField]
    private Animator _animator = null;

    [SerializeField]
    private string _animation = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayAnimation()
    {
        if (null != _animator)
        {
            _animator.Play(_animation);
            StartCoroutine(WaitForAnimationEnd());
        }
    }

    private IEnumerator WaitForAnimationEnd()
    {
        while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return new WaitForSeconds(0.1f);
        }

        animationDone.Invoke();
    }
}
