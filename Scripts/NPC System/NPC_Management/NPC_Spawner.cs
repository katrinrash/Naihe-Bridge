using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages spawning and initialization of all NPC types for a specific game shift based on the provided data.
/// </summary>

public class NPC_Spawner : MonoBehaviour
{
    //Data References
    private ShiftData_SO _shiftData;
    private SpawningData_SO _spawningData;

    public void Initialize(ShiftData_SO shiftData, SpawningData_SO spawnData)
    {
        _shiftData = shiftData;
        _spawningData = spawnData;
    }

    public NPC[] CreateCustomers()
    {
        var customers = new NPC[_shiftData.MaxCustomers];

        for (int i = 0; i < customers.Length; i++)
        {
            NPC npc = Instantiate(_spawningData.NpcPrefab, transform);
            npc.gameObject.SetActive(false);
            npc.SetUpBaseComponents(_spawningData.NpcOptions[RandomUtility.GetRandomInt(0, _spawningData.NpcOptionsLength)]);

            customers[i] = npc;
        }

        return customers;
    }

    public SpecialGuest CreateSpecialGuest()
    {
        SpecialGuest guest = Instantiate(_spawningData.SpecialGuestPrefab, transform);
        guest.gameObject.SetActive(false);
        guest.SetUpBaseComponents(_shiftData.SpecialGuest);

        return guest;
    }

}
