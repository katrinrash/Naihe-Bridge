
/// <summary>
/// Main controller for individual special guests, managing their behaviour, ordering flow, and general lifecycle.
/// </summary>

public class SpecialGuest : NPC_Base 
{
    // Specific Data
    private SG_Data_SO SG_Data => _npcData  as SG_Data_SO;
    private FinalResultEnum _result;
    private int _currentIndex = 0;

    #region Overriden Methods
    
    public override void StartOrdering()
    {
        OrderEventBus.OnOrderResultCalculated += SetRes;
        DialogueManager.OnDialogueCompleted += HandleOnDialogueCompleted;

        NPC_EventBus.TriggerDialogue(SG_Data.GetDialogue(_currentIndex).DialogueLines, SG_Data);
    }

    public override void ReceiveOrder()
    {
        NPC_EventBus.TriggerDialogue(SG_Data.GetDialogue(_result, _currentIndex).DialogueLines, SG_Data);
        Enable(physicsIncluded: false);
    }

    protected override void OnCustomerRemoved()
    {
        OrderEventBus.OnOrderResultCalculated -= SetRes;

        _currentIndex++;
        NPC_EventBus.FinishOrder();
        _majorManager.CustomerCompleted();

        base.OnCustomerRemoved();
    }

    #endregion

    private void HandleOnDialogueCompleted()
    {
        if (!_ordered)
            OrderPlaced();
        else
        {
            DialogueManager.OnDialogueCompleted -= HandleOnDialogueCompleted;
            RemoveCustomer(_majorManager.GetEndPosition());
            return;
        }
    }

    private void SetRes(FinalResultEnum res) => _result = res;

}
