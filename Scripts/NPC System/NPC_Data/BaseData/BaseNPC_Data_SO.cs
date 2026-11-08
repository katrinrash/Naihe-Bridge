using UnityEngine;

/// <summary>
/// Base data storage for all NPC types, including visual prefab and configurable movement settings.
/// </summary>

public abstract class BaseNPC_Data_SO : ScriptableObject
{
    //Base Data
    [SerializeField] protected GameObject npcPrefab;

    //Movement 
    [SerializeField] protected float maxMovementSpeed = 3.5f;
    [SerializeField] protected float minMovementSpeed = 2.5f;
    [SerializeField] protected float stopOffset = 0.5f;
    [SerializeField] protected float rotationSpeed = 8f;

    //Public Access to the Data
    public GameObject NpcPrefab => npcPrefab;
    public float StopOffset => stopOffset;
    public float RotationSpeed => rotationSpeed;

    public float GetSpeed()
    {
        return RandomUtility.GetRandomFloat(minMovementSpeed, maxMovementSpeed);
    }
}
