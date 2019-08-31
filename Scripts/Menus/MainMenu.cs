﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private string _firstScene = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartButtonClicked()
    {
        FindObjectOfType<TransitionEffect>().FadeOut();
        Invoke("ChangeScene", 1f);
    }

    private void ChangeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_firstScene);
    }
}
