using System.Collections;
using UnityEngine;

/// <summary>
/// Manages the global cooking timer, tracking order preparation time, checking whether the allowed time limit has been exceeded 
/// to trigger the corresponding response when the time limit is reached.
/// </summary>

public class CookingTimeIndicator : IndicatorBaseLogic 
{
    [Header("Time Limits Data")]
    [SerializeField] private float waitingLimit = 85f;
    [SerializeField] private float peakWaitingLimit = 70f;

    private bool _special;
    private float _currentCookingTime;
    private float _waitingLimit;

    #region Life Cycle

    protected override void Awake()
    {
        base.Awake();
        MinigamesManager.OnMinigameCompleted += HideIndicator;
        NPC_EventBus.OnOrderPlaced += ActivateIndicator;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        MinigamesManager.OnMinigameCompleted -= HideIndicator;
        NPC_EventBus.OnOrderPlaced -= ActivateIndicator;
    }

    #endregion

    protected override void SetupProperties(StagesEnum stage)
    {
        _special = stage == StagesEnum.Special;
        _waitingLimit = stage == StagesEnum.Peak ? peakWaitingLimit : waitingLimit;
    }

    #region Push Specific Logic

    private void ActivateIndicator()
    {
        _currentCookingTime = 0f;
        enabled = true;
    }

    public void UpdateTimeLimit(int ordersCount) => _waitingLimit *= ordersCount;

    private void Update() => _currentCookingTime += Time.deltaTime;
    private void InactivateIndicator() => enabled = false;
    public bool IsTimeExceeded() => _currentCookingTime >= waitingLimit;

    #endregion

    public override void Initialize()
    {
        if(!_special) 
        {
            indicator.SetActive(true);
            _fillAmount = _currentCookingTime / _waitingLimit;
            fillImage.fillAmount = _fillAmount;
            fillImage.gameObject.SetActive(true);
            StartProcess();
        }
        else if(indicator.activeSelf)
            HideIndicator();
    }

    protected override IEnumerator IncreaseIndicator()
    {
        while (_fillAmount < 1)
        {
            _fillAmount = _currentCookingTime / _waitingLimit;
            fillImage.fillAmount = _fillAmount;
            yield return null;
        }
    }

    public override void HideIndicator()
    {
        InactivateIndicator();
        StopProcess();
        fillImage.gameObject.SetActive(false);
        indicator.SetActive(false);
    }

}
