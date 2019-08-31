using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectState
{
    public enum ObjectStates
    {
        Default,
        Dead,
    };

    private Vector2 _position = Vector2.zero;
    private ObjectStates _state = ObjectStates.Default;

    public Vector2 Position
    {
        get { return _position; }
        set { _position = value; }
    }

    public ObjectStates State
    {
        get { return _state; }
        set { _state = value; }
    }
}
