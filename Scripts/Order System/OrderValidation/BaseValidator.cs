using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for the order validation process.
/// </summary>

[Serializable]
public abstract class BaseValidator
{
    [SerializeField] private List<ValidationDataSO> requirements;

    protected ValidationDataSO _currentDish;
    protected NPC_Base _currentCustomer;
    protected bool _sgStage;
    protected int _result;
    protected int _maxPoints;
    protected int _currentPoints;

    #region Initialization

    public void SetupValidator(DishesEnum dish, NPC_Base customer, bool sgStage)
    {
        _currentDish = requirements.Find(req => req.GetDish() == dish);
        _currentCustomer = customer;
        _sgStage = sgStage;
        _maxPoints = _currentDish.GetRequiredPoints();
        _currentPoints = 0;
        _result = 0;
    }

    public virtual void Subscribe() { }
    public virtual void Unsubscribe() { }

    #endregion

    #region Result Management

    public int GetResult()
    {
        CountResult();
        return _result;
    }
    protected virtual void CountResult() { }

    public bool IsValidResult() => _currentPoints == _maxPoints;

    #endregion

    protected void SetSuccessfulValidation()
    {
        _currentPoints++;
    }
}
