using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Main manager for the order system, controlling the overall order flow.
/// </summary>

public class OrderManager : MonoBehaviour
{
    [SerializeField] private PushingManager pushingManager;
    [SerializeField] private QueueManager queueManager;
    [SerializeField] private NPC_VisualCommunicationData_SO dishesData;

    private NPC_OrderController _ordersController;
    private NPC_Base _currentCustomer;
    private int _ordersCount = 0;
    private int _readyOrdersCount = 0;

    #region Subscriptions

    private void Start()
    {
        _ordersController = new NPC_OrderController(dishesData.InfoImages.Length - 1);

        InputManager.Instance.InteractionAction.performed += ReactOnInput;
        MinigamesManager.OnMinigameCompleted += MarkOrderAsReady;
        NPC_EventBus.OnOrderFinished += CheckOutCustomer;
    }

    private void OnDestroy()
    {
        InputManager.Instance.InteractionAction.performed -= ReactOnInput;
        MinigamesManager.OnMinigameCompleted -= MarkOrderAsReady;
        NPC_EventBus.OnOrderFinished -= CheckOutCustomer;
    }

    private void ReactOnInput(InputAction.CallbackContext context)
    {
        if (!_currentCustomer)
            TryTakeAnOrder();
    }

    #endregion

    private void TryTakeAnOrder()
    {
        if (!CameraPositionsManager.Instance.IsInServePosition) return;
        else if (queueManager.GetCustomersCount() == 0 || !queueManager.GetCustomer(0).HasTheRightDistance) return;

        _currentCustomer = queueManager.GetCustomer(0);

        OrderEventBus.ManageOrder(_currentCustomer);
        OrderEventBus.ManageOrderState(availability: true);
        _readyOrdersCount = 0;

        ManageCustomerSpecificBehavior();
    }

    private void ManageCustomerSpecificBehavior()
    {
        SG_Data_SO sgData = _currentCustomer.GetData() as SG_Data_SO;
        DishesEnum dish = sgData ? sgData.Dish : DishesEnum.None;
        (int index, int count) = _ordersController.SetupNPCOrder(dish);

        if (!sgData)
        {
            NPC customer = _currentCustomer as NPC;

            if (count == 1)
                customer.SetSpecificData(dishesData, index);
            else
                customer.SetSpecificData(dishesData, index, _ordersController.GetExtraDish());
        }

        _ordersCount = count;
        pushingManager.UpdateRequirements(count);
        _currentCustomer.StartOrdering();
    }

    private void MarkOrderAsReady()
    {
        _readyOrdersCount++;

        if (_readyOrdersCount == _ordersCount)
        {
            OrderEventBus.ManageOrderState(availability: false);
            OrderEventBus.ManageOrderCompletion();
        }
    }

    private void CheckOutCustomer()
    {
        queueManager.DequeueCustomer(); 
        _ordersController.ResetAfterReady();
        OrderEventBus.ManageOrderState(availability: false);
        _currentCustomer = null;
    } 

}
