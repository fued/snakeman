using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager
{
    private static GameStateManager _instance = null;

    private Dictionary<int, ObjectState> _objectStateData = new Dictionary<int, ObjectState>();

    private int _killCount = 0;

    public void AddKill()
    {
        ++_killCount;
    }

    public int GetKills()
    {
        return _killCount;
    }

    private int _clueCount = 0;

    public void AddClue()
    {
        ++_clueCount;
    }

    public int GetClues()
    {
        return _clueCount;
    }

    public static GameStateManager Get()
    {
        if (null == _instance)
        {
            _instance = new GameStateManager();
        }

        return _instance;
    }

    public enum GameStates
    {
        Game,
        Dialogue,
        Menu,
    };

    private GameStates _state = GameStates.Game;
    private GameStates _prevState = GameStates.Menu;

    public GameStates State
    {
        get { return _state; }
    }

    public void StartGame()
    {
        _prevState = _state;
        _state = GameStates.Game;
        Time.timeScale = 1f;
    }

    public void OpenMenu()
    {
        _prevState = _state;
        _state = GameStates.Menu;
        Time.timeScale = 0f;
    }

    public void CloseMenu()
    {
        GameStates tmp = _state;

        _state = _prevState;
        if (_state == GameStates.Game)
        {
            Time.timeScale = 1f;
        }

        _prevState = tmp;
    }

    public void StartDialogue()
    {
        _prevState = _state;
        _state = GameStates.Dialogue;
        Time.timeScale = 0f;
    }

    public void EndDialogue()
    {
        _prevState = _state;
        _state = GameStates.Game;
        Time.timeScale = 1f;
    }

    public ObjectState GetObjectState(int ID)
    {
        ObjectState state = null;

        if (_objectStateData.ContainsKey(ID))
        {
            state = _objectStateData[ID];
        }

        return state;
    }

    public void SetState(int ID, ObjectState state)
    {
        _objectStateData[ID] = state;
    }
}
