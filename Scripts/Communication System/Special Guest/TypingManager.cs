using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages dialogue text display through a typewriter effect, adapting typing speed and progression based on player input.
/// </summary>

public class TypingManager
{
    #region Variables

    // References
    private readonly DialogueManager _dialogueManager;
    private readonly TextMeshProUGUI _text;

    // Typing Speed Variables
    private readonly float _tOffset;
    private readonly float _multiplier;
    private float _currentTOffset;

    // Typing General Variables
    private bool _typingCompleted;
    private string _context;
    private int _characterCount;
    private int _characters;

    #endregion

    public TypingManager(DialogueManager manager, TextMeshProUGUI t, float offset, float multiplier)
    { 
        _dialogueManager = manager;
        _text = t;

        _tOffset = offset;
        _multiplier = multiplier;
    }
    
    public void StartTyping(string c)
    {
        _context = c;
        _typingCompleted = false;

        _currentTOffset = _tOffset;
        _characterCount = 0;
        _characters = 0;

        InputManager.Instance.ValidationAction.performed += HandleMultiplying;
        _dialogueManager.StartTypingCoroutine(Type());
    }

    public bool IsTypingCompleted() => _typingCompleted;

    private IEnumerator Type()
    {
        _text.maxVisibleCharacters = 0;
        _text.text = _context;

        _text.ForceMeshUpdate();
        _characterCount = _text.textInfo.characterCount;

        for (;_characters < _characterCount; _characters++)
        {
            _text.maxVisibleCharacters = _characters + 1;
            yield return new WaitForSeconds(_currentTOffset);
        }

        _typingCompleted = true;
        InputManager.Instance.ValidationAction.performed -= HandleMultiplying;
    }

    private void HandleMultiplying(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (_currentTOffset == _tOffset / _multiplier)
            _characters = _characterCount - 2;
        else
            _currentTOffset = _tOffset / _multiplier;
    }

}
