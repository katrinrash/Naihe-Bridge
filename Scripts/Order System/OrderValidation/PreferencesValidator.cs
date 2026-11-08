using System;

/// <summary>
/// Specific validator for customer dietary and spice preferences.
/// </summary>

[Serializable]
public class PreferencesValidator : BaseValidator
{
    private ProductSO _currentProduct;

    #region Initialization

    public override void Subscribe() 
    {
        MG_EventBus.OnIngredientsValidation += ValidateIngredient;
        MG_EventBus.OnSpicesValidation += ValidateSpices;
    }
    public override void Unsubscribe() 
    {
        MG_EventBus.OnIngredientsValidation -= ValidateIngredient;
        MG_EventBus.OnSpicesValidation -= ValidateSpices;
    }

    #endregion

    protected override void CountResult()
    {
        _result = _currentPoints == _maxPoints ? (int)QualityCheckEnum.CorrectIngredient : (int)QualityCheckEnum.IncorrectIngredient;
    }

    private bool ConditionsCheck(bool spices)
    {
        if (_currentCustomer)
        {
            if (_sgStage)
            {
                ValidateSGPreferences(spices);
                return false;
            }
            else
                return true;
        }
        else
            SetSuccessfulValidation();

        return false;
    }

    #region Validation

    private void ValidateIngredient(ProductSO product)
    {
        _currentProduct = product;

        if (!ConditionsCheck(spices: false)) return;

        NPC_Data_SO _currentNpcData = (NPC_Data_SO)_currentCustomer.GetData();
        if (_currentNpcData.FeedingType == product.PreferencesType) SetSuccessfulValidation();
    }

    private void ValidateSpices(ProductSO product)
    {
        _currentProduct = product;

        if (!ConditionsCheck(spices: true)) return;

        NPC_Data_SO _currentNpcData = (NPC_Data_SO)_currentCustomer.GetData();
        if (_currentNpcData.SpicesType == product.SpicesType) SetSuccessfulValidation();
    }

    private void ValidateSGPreferences(bool spices)
    {
        SG_Data_SO _currentNpcData = (SG_Data_SO)_currentCustomer.GetData();

        if (_currentNpcData.Preferences.Contains(_currentProduct))
        {
            SetSuccessfulValidation();

            if(spices)
                SpecialGuestProgressManager.Instance.UnlockSpices(_currentNpcData, _currentProduct);
            else
                SpecialGuestProgressManager.Instance.UnlockIngredient(_currentNpcData, _currentProduct);
        }

    }

    #endregion

}
