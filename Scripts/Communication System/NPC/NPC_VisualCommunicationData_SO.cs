using UnityEngine;

/// <summary>
/// Data storage for regular NPCs communication data, including customer orders and their reactions to prepared dishes.
/// </summary>

[CreateAssetMenu(fileName = "VisualCommunication_SO", menuName = "NPC_SO/VisualCommunicationData")]
public class NPC_VisualCommunicationData_SO : ScriptableObject
{
    //Base Data
    [SerializeField] private Sprite[] background;
    [SerializeField] private Sprite[] infoImages;

    //Public Access to the Data
    public Sprite[] Background => background;
    public Sprite[] InfoImages => infoImages;
}
