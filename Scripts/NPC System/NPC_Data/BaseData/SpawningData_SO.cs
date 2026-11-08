using UnityEngine;

/// <summary>
/// Data storage for NPC spawning configuration, including customer options and prefabs.
/// </summary>

[CreateAssetMenu(fileName = "SpawningData", menuName = "NPC_SO/SpawningData")]
public class SpawningData_SO : ScriptableObject
{
    [SerializeField] private NPC_Data_SO[] npcOptions;
    [SerializeField] private NPC npcPrefab;

    [SerializeField] private SpecialGuest specialGuestPrefab;

    public NPC_Data_SO[] NpcOptions => npcOptions;
    public int NpcOptionsLength => npcOptions?.Length ?? 0;
    public NPC NpcPrefab => npcPrefab;

    public SpecialGuest SpecialGuestPrefab => specialGuestPrefab;
}
