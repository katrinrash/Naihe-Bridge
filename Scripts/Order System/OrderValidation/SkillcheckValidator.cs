using System;

/// <summary>
/// Specific validator for cooking minigames skill checks.
/// </summary>

[Serializable]
public class SkillcheckValidator : BaseValidator
{
    #region Initialization

    public override void Subscribe()
    {
        MG_EventBus.OnSkillCheckSuccessful += SetSuccessfulValidation;
    }
    public override void Unsubscribe()
    {
        MG_EventBus.OnSkillCheckSuccessful -= SetSuccessfulValidation;
    }

    #endregion

    protected override void CountResult()
    {
        _result = _currentPoints == _maxPoints ? (int)QualityCheckEnum.GoodSkillCheck : (int)QualityCheckEnum.BadSkillCheck;
    }
}
