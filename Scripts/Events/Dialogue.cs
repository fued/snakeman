using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour
{
    private const float PORTRAIT_SIZE = 64f;

    public UnityEvent closed;

    public static Dialogue SpawnDialogue(Rect sizeAndPos, string message, Sprite portraitImage = null)
    {
        GameObject dialoguePrefab = Resources.Load<GameObject>("Prefabs/Dialogue");

        Canvas root = FindObjectOfType<Canvas>();
        GameObject dialogueObject = Instantiate<GameObject>(dialoguePrefab, root.transform);

        Dialogue dialogue = dialogueObject.GetComponent<Dialogue>();

        dialogue.SizeDelta = sizeAndPos;
        dialogue.Portrait = portraitImage;
        dialogue.Text = message;

        return dialogue;
    }

    [SerializeField]
    private RectTransform _rectTransform = null;

    [SerializeField]
    private TMP_Text _textBox = null;

    [SerializeField]
    private Image _portrait = null;

    private Sprite _portraitImage = null;

    [SerializeField]
    private string _text = "";

    [SerializeField]
    private float _textSpeed = 0.2f;

    private float _timePassed = 0f;
    private float _lastRealTime = 0f;
    private int charIdx = 0;

    [SerializeField]
    private KeyCode _skipKey = KeyCode.Escape;

    public Rect SizeDelta
    {
        get { return _rectTransform.rect; }
        set
        {
            _rectTransform.sizeDelta = new Vector2(value.width, value.height);
            _rectTransform.anchoredPosition = new Vector2(value.x, value.y);
            Setup();
        }
    }

    public string Text
    {
        get { return _text; }
        set
        {
            _lastRealTime = Time.realtimeSinceStartup;
            _timePassed = 0f;
            charIdx = 0;
            _text = value;
            _textBox.text = "";
        }
    }

    public Sprite Portrait
    {
        get { return _portraitImage; }
        set
        {
            _portraitImage = value;
            Setup();
        }
    }

    private void Setup()
    {
        if (null != _portraitImage)
        {
            _portrait.gameObject.SetActive(true);
            _portrait.sprite = _portraitImage;
        }
        else
        {
            _portrait.gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameStateManager.Get().StartDialogue();

        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaRealTime = 0f;

        if (GameStateManager.GameStates.Dialogue == GameStateManager.Get().State)
        {
            deltaRealTime = Time.realtimeSinceStartup - _lastRealTime;
        }

        _lastRealTime = Time.realtimeSinceStartup;

        if (Input.GetKeyDown(_skipKey))
        {
            if (charIdx >= _text.Length)
            {
                closed.Invoke();
                Destroy(this.gameObject);
                GameStateManager.Get().EndDialogue();
                return;
            }
            else
            {
                _textSpeed = 0f;
            }
        }

        if (charIdx < _text.Length)
        {
            _timePassed += deltaRealTime;

            while (((charIdx + 1) * _textSpeed < _timePassed) && (charIdx < _text.Length))
            {
                _textBox.text += _text[charIdx];
                ++charIdx;
            }
        }
    }
}
