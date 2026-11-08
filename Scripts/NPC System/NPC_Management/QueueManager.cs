using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the customer queue.
/// </summary>

public class QueueManager : MonoBehaviour  
{
    public static event Action<NPC_Base, Transform> OnPositionChanged; 
    public static event Action<bool> OnQueueUpdated;

    [SerializeField] private List<Transform> queuePositions; 
    private List<NPC_Base> _customers = new(); 

    #region Queue Management  

    public void EnqueueCustomer(NPC_Base npc)
    {
        _customers.Add(npc);
        UpdateQueuePosition();

        if(_customers.Count == 1) 
        {
            OnQueueUpdated?.Invoke(true);
        }
    }

    public void DequeueCustomer()
    {
        if (_customers.Count == 0) 
        {
            OnQueueUpdated?.Invoke(false);
            return; 
        }

        ClearFirstCustomer();
        UpdateQueuePosition();
    }

    private void UpdateQueuePosition()
    {
        for (int i = 0; i < _customers.Count; i++)
            OnPositionChanged?.Invoke(_customers[i], queuePositions[i]);
    }

    private void ClearFirstCustomer()
    {
        _customers[0].ClearSubscriptions();
        _customers.RemoveAt(0);
    }

    #endregion

    #region Getters
    
    public int GetCustomersCount() => _customers.Count; 
     
    public NPC_Base GetCustomer(int index)
    {
        if(index >= _customers.Count || index < 0)
            return null;

        return _customers[index];
    }

    #endregion
}
