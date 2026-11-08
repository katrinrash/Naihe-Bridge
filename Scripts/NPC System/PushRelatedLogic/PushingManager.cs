using System.Collections;
using UnityEngine;

/// <summary>
/// Manages the customer elimination process, checking cooking time conditions when serving orders and triggering the corresponding response when exceeded.
/// </summary>

public class PushingManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private QueueManager queueManager;
    [SerializeField] private PushingAnimationController pushingAnimationController;
    [SerializeField] private CookingTimeIndicator globalTimer;

    private NPC _victim;
    private NPC _killer;

    private void Start() => enabled = false;

    private void Update()
    {
        if (_killer.HasTheRightDistance)
        {
            pushingAnimationController.Eliminate();
            enabled = false;
        }
    }

    public void UpdateRequirements(int ordersCount) => globalTimer.UpdateTimeLimit(ordersCount); 

    public bool IsWaitingTimeExceeded()
    {
        if (queueManager.GetCustomersCount() <= 1) return false;
        return globalTimer.IsTimeExceeded();
    }

    public void PushCustomer()
    {
        _victim = (NPC)queueManager.GetCustomer(0);
        _killer = (NPC)queueManager.GetCustomer(1);
        pushingAnimationController.Initialize(_victim, _killer, queueManager, this);

        _victim.InactivateCustomer();
        _killer.UpdateMovement();

        InputManager.Instance.InteractionAction.Disable();
        pushingAnimationController.PickUp();
    }

    public void OnPreparationCompleted() => enabled = true;

}


