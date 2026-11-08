using System;
using UnityEngine;

/// <summary>
/// Helper class used by NPC_Manager to store and manage NPC-related data.
/// </summary>

[Serializable]
public class NPC_DataManagement 
{
    [Header("Spawning Management")]
    [SerializeField] private SpawningData_SO spawningData;
    [SerializeField] private NPC_Spawner npcSpawner;

    [Header("Destination Data")]
    [SerializeField] private Transform[] tables;
    [SerializeField] private Transform endPositionTransform;

    [Header("Additional Data")]
    [SerializeField] private float tableTime = 40f;

    private int _index = 0;

    public NPC_Spawner InitializeSpawner(ShiftData_SO data)
    {
        npcSpawner.Initialize(data, spawningData);
        return npcSpawner;
    }

    public (Transform spot, float time) GetTableData()
    {
        if(_index >= tables.Length)
            _index = 0;

        return (tables[_index++], tableTime);
    }

    public Transform GetEndPosition()
    { 
        return endPositionTransform;
    }
}
