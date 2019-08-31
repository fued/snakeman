using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueSpawner : MonoBehaviour
{
    public UnityEvent finished;

    [SerializeField]
    private Sprite _portrait = null;

    [SerializeField][TextArea]
    private string _text = "";

    public void SpawnDialogue()
    {
        Dialogue.SpawnDialogue(new Rect(0f, 220f, 1280f, 280f), _text, _portrait).closed.AddListener(Done);
    }

    public void Done()
    {
        finished.Invoke();
    }
}
