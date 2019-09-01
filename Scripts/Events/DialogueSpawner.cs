using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueSpawner : MonoBehaviour
{
    public UnityEvent finished;

    [SerializeField]
    public Sprite _portrait = null;

    [SerializeField][TextArea]
    public string _text = "";

    [SerializeField]
    private AudioManager.SFX _dialogSound = AudioManager.SFX.Dialogue;

    public void SpawnDialogue()
    {
        Dialogue.SpawnDialogue(new Rect(0f, 220f, 1280f, 280f), _text, _portrait, _dialogSound).closed.AddListener(Done);
    }

    public void Done()
    {
        finished.Invoke();
    }
}
