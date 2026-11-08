using System;
using System.Collections.Generic;

/// <summary>
/// Central event bus for NPC-related communication.
/// </summary>

public class NPC_EventBus
{
    public static event Action<List<string>, SG_Data_SO> OnDialogueHandled;

    //Order Events
    public static event Action OnOrderProcessing;
    public static event Action OnOrderPlaced;
    public static event Action OnOrderFinished;

    //Movement Events
    public event Action OnMovementStarted;
    public event Action OnMovementStopped;

    //Visual Communication Events
    public static event Action OnInfoStateChanged;
    public event Action<NPC_VisualCommunicationData_SO, int> ShowInformation;
    public event Action<NPC_VisualCommunicationData_SO, int, int> ShowMultipleInformation;
    public event Action HideInformation;

    // Static Event Triggers
    public static void TriggerDialogue(List<string> text, SG_Data_SO guest) => OnDialogueHandled?.Invoke(text, guest);
    public static void ChangeInfoState() => OnInfoStateChanged?.Invoke();
    public static void ProcessOrder() => OnOrderProcessing?.Invoke();
    public static void PlaceOrder() => OnOrderPlaced?.Invoke();
    public static void FinishOrder() => OnOrderFinished?.Invoke();

    // Event Triggers
    public void StartMovement() => OnMovementStarted?.Invoke();
    public void StopMovement() => OnMovementStopped?.Invoke();
    public void ShowInfo(NPC_VisualCommunicationData_SO data, int index) => ShowInformation?.Invoke(data, index);
    public void ShowMultipleInfo(NPC_VisualCommunicationData_SO data, int index, int secondIndex) => ShowMultipleInformation?.Invoke(data, index, secondIndex);
    public void HideInfo() => HideInformation?.Invoke();
}
