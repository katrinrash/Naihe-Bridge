using System;

/// <summary>
/// Central event bus for order-related communication.
/// </summary>

public static class OrderEventBus
{
    //NPC ordering events
    public static event Action<DishesEnum> OnDishChosen;
    public static event Action<bool> ResetAfterOrderIsReady;

    //General order events
    public static event Action<NPC_Base> OnOrdered;
    public static event Action<bool> OnOrderStateChanged;
    public static event Action OnOrderCompleted;
    public static event Action<FinalResultEnum> OnOrderResultCalculated;

    #region Event Management Methods

    public static void ManageDishChosen(DishesEnum dish) => OnDishChosen?.Invoke(dish);
    public static void ManageResetAfterOrderIsReady(bool reset) => ResetAfterOrderIsReady?.Invoke(reset);
    public static void ManageOrder(NPC_Base customer) => OnOrdered?.Invoke(customer);
    public static void ManageOrderState(bool availability) => OnOrderStateChanged?.Invoke(availability);
    public static void ManageOrderCompletion() => OnOrderCompleted?.Invoke();
    public static void ManageOrderResult(FinalResultEnum result) => OnOrderResultCalculated?.Invoke(result);

    #endregion
}
