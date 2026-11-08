using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// Main manager for the special guest dialogue system, responding to dialogue activation by guests and managing dialogue progression, 
/// UI presentation, and completion events.
/// </summary>

public class DialogueManager : MonoBehaviour
{
    public static event Action OnDialogueCompleted;

    #region Variables

    [SerializeField, Range(0.5f, 5f)] private float _typingOffset = 1f;
    [SerializeField, Range(1f, 10f)] private float _typingMultiplier = 4f;

    [SerializeField] private CameraPositionsManager _cameraPositionsManager;
    [SerializeField] private GameObject textBox;
    [SerializeField] private TextMeshProUGUI _textArea;
    [SerializeField] private List<StructForDictionary<SG_Data_SO, GameObject>> icons;

    private const int CONST_OffsetNormalizer = 10;

    private TypingManager _typingManager;
    private List<string> text = new();
    private int _index = 0;
    private SG_Data_SO _currentGuest;

    #endregion

    #region Unity Life Cycle 

    private void Awake()
    {
        _typingManager = new TypingManager(this, _textArea, _typingOffset / CONST_OffsetNormalizer, _typingMultiplier);
        NPC_EventBus.OnDialogueHandled += StartDialogue;
    }

    private void OnDestroy()
    {
        NPC_EventBus.OnDialogueHandled -= StartDialogue;
    }

    #endregion

    private void StartDialogue(List<string> t, SG_Data_SO guest) 
    {
        InputManager.Instance.ValidationAction.performed += HandleDialogueProgression;

        text = t;
        _currentGuest = guest;
        _index = 0;
        icons.FirstOrDefault(x => x.Key == _currentGuest).Value.SetActive(true);
        textBox.SetActive(true);

        _typingManager.StartTyping(text[_index++]);
        _cameraPositionsManager.ChangeAvailability();
    }

    public void StartTypingCoroutine(IEnumerator coroutine) => StartCoroutine(coroutine);

    private void HandleDialogueProgression(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!_typingManager.IsTypingCompleted())
            return;

        ContinueDialogue();
    }

    private void ContinueDialogue()
    { 
        if(_index >= text.Count)
        {
            EndDialogue();
            return;
        }

        _typingManager.StartTyping(text[_index++]);

    }

    private void EndDialogue()
    {
        InputManager.Instance.ValidationAction.performed -= HandleDialogueProgression;
        OnDialogueCompleted?.Invoke();

        _cameraPositionsManager.ChangeAvailability();
        textBox.SetActive(false);
        icons.FirstOrDefault(x => x.Key == _currentGuest).Value.SetActive(true);
        _textArea.text = string.Empty; 
    }
}

