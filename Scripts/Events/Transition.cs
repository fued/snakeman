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
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "TitleScreen" &&UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "MainMenu" &&UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "EndGame"&&UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Death")
        {
            AudioManager.Get().PlaySfxOnce(AudioManager.SFX.Door_Open);
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(_targetScene);
    }
}
