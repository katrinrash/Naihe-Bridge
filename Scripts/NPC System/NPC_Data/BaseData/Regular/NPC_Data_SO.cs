using UnityEngine;

/// <summary>
/// Data storage for regular NPCs, including spice and dietary preferences.
/// </summary>

[CreateAssetMenu(fileName = "Regular_NPC_Data", menuName = "NPC_SO/Regular")]
public class NPC_Data_SO : BaseNPC_Data_SO
{
    //Private Data
    [SerializeField] private SpicesEnum spicesType;
    [SerializeField] private PreferencesEnum feedingType;

    //Public Access to the Data
    public SpicesEnum SpicesType => spicesType;
    public PreferencesEnum FeedingType => feedingType;
}
