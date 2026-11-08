using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Main controller for individual regular NPCs, managing their behaviour, ordering flow, communication, and table handling.
/// </summary>

public class NPC : NPC_Base 
{
    // Specific Data
    [SerializeField] private Transform carryPoint;

    private NPC_VisualCommunicationData_SO _visualData;
    private int _index;
    private int _secondIndex = -1;
    private bool _isAnimationOn = true;

    #region Input Handling

    private void OnEnable()
    {
        InputManager.Instance.ValidationAction.performed += ReactOnInput;
    }

    private void OnDisable()
    {
        InputManager.Instance.ValidationAction.performed -= ReactOnInput;
    }

    private void ReactOnInput(InputAction.CallbackContext context)
    {
        if(!_processingOrder)
            return;

        if (!_ordered)
            OrderPlaced();
        else
            GoToTheTable();
    }

    #endregion

    #region Overriden Methods

    public override void StartOrdering()
    {
        if (_secondIndex != -1)
            EventBus.ShowMultipleInfo(_visualData, _index, _secondIndex);
        else
            EventBus.ShowInfo(_visualData, _index);

        _processingOrder = true;
    }

    protected override void OrderPlaced()
    {
        EventBus.HideInfo();

        base.OrderPlaced();
    }

    public override void ReceiveOrder()
    {
        Enable(physicsIncluded: false);
        _processingOrder = true;
        EventBus.ShowInfo(_visualData, _index);
    }

    #endregion

    #region Data Management

    public void SetSpecificData(NPC_VisualCommunicationData_SO data, int index)
    {
        _visualData = data;
        _index = index;
    }
    public void SetSpecificData(NPC_VisualCommunicationData_SO data, int index, int secondIndex)
    {
        _visualData = data;
        _index = index;
        _secondIndex = secondIndex;
    }

    public Transform GetSpecificData() => carryPoint;

    #endregion

    #region Pushing Handling

    public void InactivateCustomer()
    {
        Disable(physicsIncluded: true);
        _majorManager.CustomerCompleted();
        UpdateMovement();
    }

    public void UpdateMovement(Transform newTarget = null)
    {
        if (!_isAnimationOn)
            EventBus.StartMovement();
        else 
            EventBus.StopMovement();

        _isAnimationOn = !_isAnimationOn;

        if(newTarget)
            SetDestination(newTarget);
    }

    #endregion

    #region Table Handling

    private void GoToTheTable()
    {
        EventBus.HideInfo();
        NPC_EventBus.FinishOrder();
        _processingOrder = false;

        (Transform table, float time) = _majorManager.GetTableData();
        SetDestination(table);
        StartCoroutine(SitAtTheTable(time));
    }

    private IEnumerator SitAtTheTable(float time)
    { 
        while (!HasTheRightDistance)
        {
            yield return null;
        }

        _majorManager.CustomerCompleted();

        float elapsedTime = 0f;
        while (elapsedTime <= time)
        { 
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        RemoveCustomer(_majorManager.GetEndPosition());
    }

    #endregion

}
