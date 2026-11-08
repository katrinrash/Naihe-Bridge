using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data storage for special guests dialogues.
/// </summary>

[CreateAssetMenu(fileName = "SO_Dialogue", menuName = "NPC_SO/SG_Dialogue")]
public class SO_Dialogue : ScriptableObject
{
    [SerializeField] private List<string> dialogueLines;

    public List<string> DialogueLines => dialogueLines;
}
