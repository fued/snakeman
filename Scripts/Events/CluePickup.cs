using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluePickup : MonoBehaviour
{
    public void PickUp()
    {
        GameStateManager.Get().AddClue();
        Destroy(gameObject);
    }
}
