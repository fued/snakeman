using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluePickup : MonoBehaviour
{
    public void PickUp()
    {
        GameStateManager.Get().AddClue();
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
