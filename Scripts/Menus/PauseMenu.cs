using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private KeyCode _pauseKey = KeyCode.Escape;

    [SerializeField]
    private GameObject _pauseMenu = null;

    private bool _isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject paseMenuPrefab = Resources.Load<GameObject>("Prefabs/PauseMenu");
        _pauseMenu = Instantiate<GameObject>(paseMenuPrefab, this.transform);
        _pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_pauseKey))
        {
            _isPaused = !_isPaused;

            if (_isPaused)
            {
                _pauseMenu.transform.SetAsLastSibling();
                _pauseMenu.GetComponentInChildren<TMP_Text>().text = "Clues\n" + GameStateManager.Get().GetClues() + " \\ 5";
                _pauseMenu.SetActive(true);
                GameStateManager.Get().OpenMenu();
            }
            else
            {
                _pauseMenu.SetActive(false);
                GameStateManager.Get().CloseMenu();
            }
        }
    }
}
