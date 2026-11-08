using System;

/// <summary>
/// Manages customer order generation and communicates selected dishes to other gameplay systems.
/// </summary>

public class NPC_OrderController
{
    public static event Action<DishesEnum> OnDishChosen;
    public static event Action<bool> ResetAfterOrderIsReady;

    private readonly int _maxIndex;
    private int _dishIndex;
    private int _extraDishIndex;

    public NPC_OrderController(int dishesCount) => _maxIndex = dishesCount;

    #region Order Setup

    public (int, int) SetupNPCOrder(DishesEnum dish)
    {
        int count = RandomUtility.GetRandomInt(1, 3);

        if (count == 1 || dish != DishesEnum.None)
            return SetupSingleOrder(dish, count);
        else
            return SetupMultipleOrder(count);
    }

    private (int, int) SetupSingleOrder(DishesEnum dish, int count)
    {
        if (dish != DishesEnum.None)
        {
            _dishIndex = (int)dish;
            count = 1;
        }
        else
            _dishIndex = RandomUtility.GetRandomInt(0, _maxIndex);

        OnDishChosen?.Invoke((DishesEnum)_dishIndex); 

        return (_dishIndex, count);
    }

    private (int, int) SetupMultipleOrder(int count)
    {
        //Orders Generation   
        RandomUtility.CleanCache();
        _dishIndex = RandomUtility.GetRandomUniqueInt(0, _maxIndex);
        _extraDishIndex = RandomUtility.GetRandomUniqueInt(0, _maxIndex);
        RandomUtility.CleanCache();

        OnDishChosen?.Invoke((DishesEnum)_dishIndex);
        OnDishChosen?.Invoke((DishesEnum)_extraDishIndex);

        return (_dishIndex, count);
    }

    public int GetExtraDish() => _extraDishIndex;

    #endregion

    public void ResetAfterReady()
    {
        ResetAfterOrderIsReady?.Invoke(false);
    }
}
