using UnityEngine;

/// <summary>
/// Data storage for NPC animation settings, including movement intensity and duration ranges.
/// </summary>

[CreateAssetMenu(fileName = "NPC_Animation_Data",menuName = "NPC_SO/Animation Data")]
public class NPC_Animation_Data_SO : ScriptableObject
{
    //Animation Intensity
    [SerializeField] private float minHeight = 0.08f;
    [SerializeField] private float maxHeight = 0.12f;

    //Animation Duration
    [SerializeField] private float minDuration = 0.8f;
    [SerializeField] private float maxDuration = 1.4f;

    public float GetRandomHeight()
    {
        return RandomUtility.GetRandomFloat(minHeight, maxHeight);
    }

    public float GetRandomDuration()
    {
        return RandomUtility.GetRandomFloat(minDuration, maxDuration);
    }
}
