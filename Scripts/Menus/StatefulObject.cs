using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatefulObject : MonoBehaviour
{
    [SerializeField]
    private int _uniqueID = -1;

    protected ObjectState _state = null;

    // Start is called before the first frame update
    void Start()
    {
        ObjectState startState = null;

        if (-1 != _uniqueID)
        {
            startState = GameStateManager.Get().GetObjectState(_uniqueID);
        }

        if (null == startState)
        {
            _state = new ObjectState();
            _state.Position = transform.position;
        }
        else
        {
            _state = startState;
            transform.position = _state.Position;
        }
    }

    public void SaveState()
    {
        _state.Position = transform.position;
        GameStateManager.Get().SetState(_uniqueID, _state);
    }
}
