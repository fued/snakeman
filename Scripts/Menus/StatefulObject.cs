using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatefulObject : MonoBehaviour
{
    public static ObjectState GetState(GameObject obj)
    {
        StatefulObject state = obj.GetComponent<StatefulObject>();
        ObjectState objState = null;

        if (null == state)
        {
            objState = new ObjectState();
        }
        else
        {
            objState = state.State;
        }

        return objState;
    }

    public static void UpdateState(GameObject obj, ObjectState objState)
    {
        StatefulObject state = obj.GetComponent<StatefulObject>();

        if (null != state)
        {
            state.State = objState;
        }
    }

    [SerializeField]
    private int _uniqueID = -1;

    public int UniqueID
    {
        get { return _uniqueID; }
    }

    private bool _isSetup = false;

    protected ObjectState _state = null;

    public ObjectState State
    {
        get
        {
            Setup();
            return _state;
        }
        set { _state = value; }
    }

    public void Setup()
    {
        if (!_isSetup)
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

            _isSetup = true;
        }
    }

    // Start is called before the first frame update
    public void Start()
    {
        Setup();
    }

    public void SaveState()
    {
        if (-1 != _uniqueID)
        {
            _state.Position = transform.position;
            GameStateManager.Get().SetState(_uniqueID, _state);
        }
    }
}
