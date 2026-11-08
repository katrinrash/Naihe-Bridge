using System;

/// <summary>
/// Central event bus for order-related communication.
/// </summary>

public static class OrderEventBus
{
    public static event Action<NPC_Base> OnOrdered;
    public static event Action<bool> OnOrderStateChanged;
    public static event Action OnOrderCompleted;
    public static event Action<FinalResultEnum> OnOrderResultCalculated;  

    #region Event Management Methods

    public static void ManageOrder(NPC_Base customer) => OnOrdered?.Invoke(customer);
    public static void ManageOrderState(bool availability) => OnOrderStateChanged?.Invoke(availability);
    public static void ManageOrderCompletion() => OnOrderCompleted?.Invoke();
    public static void ManageOrderResult(FinalResultEnum result) => OnOrderResultCalculated?.Invoke(result);

    #endregion
}
