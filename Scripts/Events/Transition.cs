using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    [SerializeField]
    private string _targetScene = "";

    public void OnTriggered()
    {
        foreach (var obj in FindObjectsOfType<StatefulObject>())
        {
            obj.SaveState();
        }

        FindObjectOfType<TransitionEffect>().FadeOut();
        Invoke("ChangeScene", 1f);
    }

    private void ChangeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_targetScene);
    }
}
