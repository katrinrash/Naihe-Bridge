using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Main manager for order validation, evaluating cooking results based on skill checks and customer preferences, 
/// calculating the final order outcome, and notifying related systems.
/// </summary>

public class OrderValidator : InstanceBaseClass<OrderValidator> 
{
    [SerializeField] private PushingManager _pushingManager;

    [Header("Validators")]
    [SerializeField] private SkillcheckValidator skillcheckValidator;
    [SerializeField] private PreferencesValidator preferenceValidator;
    [Header("Result Data")]
    [SerializeField] private List<StructForDictionary<FinalResultEnum, int>> reactionValues;
    [SerializeField] private NPC_VisualCommunicationData_SO reactionData;

    //General Private Data
    private NPC_Base _currentCustomer;
    private TriggerZoneLogic _currentZone;
    private bool _special;

    //Validation Private Data 
    private FinalResultEnum _currentOrderResult = FinalResultEnum.None;
    private bool _readyToValidate;
    private int _skillCheck;
    private int _preferencesCheck;

    #region Unity Life Cycle

    protected override void OnInstanceBaseClassAwake()
    {
        InputManager.Instance.InteractionAction.performed += ReactOnInput;
        ShiftManager.OnStageInitialized += SetStageState;
        OrderEventBus.OnOrdered += SetCurrentCustomer;
        OrderEventBus.OnOrderCompleted += SetValidationReadiness;
        TriggerZoneLogic.OnTriggerZoneUpdated += SetCurrentZone;
        MinigamesManager.OnMinigameStarted += SetupValidation;
        MinigamesManager.OnMinigameCompleted += SetOrderResult;

        skillcheckValidator.Subscribe();
        preferenceValidator.Subscribe();
    }

    private void OnDestroy()
    {
        InputManager.Instance.InteractionAction.performed -= ReactOnInput;
        ShiftManager.OnStageInitialized -= SetStageState;
        OrderEventBus.OnOrdered -= SetCurrentCustomer;
        OrderEventBus.OnOrderCompleted -= SetValidationReadiness;
        TriggerZoneLogic.OnTriggerZoneUpdated -= SetCurrentZone;
        MinigamesManager.OnMinigameStarted -= SetupValidation;
        MinigamesManager.OnMinigameCompleted -= SetOrderResult;

        skillcheckValidator.Unsubscribe();
        preferenceValidator.Unsubscribe();
    }

    #endregion

    #region Setup Methods

    private void SetupValidation()
    {
        skillcheckValidator.SetupValidator(_currentZone.GetOrderedDish(), _currentCustomer, _special);
        preferenceValidator.SetupValidator(_currentZone.GetOrderedDish(), _currentCustomer, _special);
    }

    private void SetCurrentZone(TriggerZoneLogic zone) => _currentZone = zone;
    private void SetCurrentCustomer(NPC_Base customer) => _currentCustomer = customer;
    private void SetStageState(StagesEnum stage) => _special = stage == StagesEnum.Special;
    private void SetValidationReadiness() => _readyToValidate = true;

    #endregion

    private void ReactOnInput(InputAction.CallbackContext context)
    {
        if (!_readyToValidate) return;

        CheckTheDish();
    }

    #region Order Result Management

    private void CheckTheDish()
    {
        if (!CameraPositionsManager.Instance.IsInServePosition) return;

        _readyToValidate = false;

        if (_pushingManager.IsWaitingTimeExceeded())
        {
            _pushingManager.PushCustomer();
            return;
        }

        InformCustomer();
        _currentOrderResult = FinalResultEnum.None;
    }

    private void InformCustomer()
    {
        if (_currentCustomer is NPC customer)
        {
            int v = reactionValues.FirstOrDefault(x => x.Key == _currentOrderResult).Value;
            customer.SetSpecificData(reactionData, v);
        }

        _currentCustomer.ReceiveOrder();
    }

    private void SetOrderResult()
    {
        CountResultPoints();

        var result = (FinalResultEnum)(_skillCheck + _preferencesCheck);

        if (_currentOrderResult == FinalResultEnum.None)
            _currentOrderResult = result;
        else if (_currentOrderResult != result)
            _currentOrderResult = FinalResultEnum.AverageOrder;

        OrderEventBus.ManageOrderResult(_currentOrderResult);
    }

    private void CountResultPoints()
    {
        _skillCheck = skillcheckValidator.GetResult();
        _preferencesCheck = preferenceValidator.GetResult();
    }

    public int GetDishResultIndex()
    {
        if (skillcheckValidator.IsValidResult() && preferenceValidator.IsValidResult()) return 0;
        else return 1;
    }

    #endregion
}
