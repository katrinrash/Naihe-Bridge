using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Base class for NPCs and special guests functionality.
/// </summary>

[RequireComponent(typeof(NavMeshAgent))]
public abstract class NPC_Base : MonoBehaviour 
{
    #region References and Data

    //Properties
    public NPC_EventBus EventBus { get; private set; } = new NPC_EventBus();
    public bool HasTheRightDistance => _physicsManager.HasTheRightDistance;

    //References
    protected NPC_Manager _majorManager;
    protected Transform _currentTarget;
    protected BaseNPC_Data_SO _npcData;
    private NPC_PhysicsManager _physicsManager;

    //Booleans
    protected bool _ordered = false;
    protected bool _processingOrder = false;

    #endregion

    #region Initialization

    //Happens once
    public void SetUpBaseComponents(BaseNPC_Data_SO data)
    {
        _npcData = data;
        Instantiate(_npcData.NpcPrefab, gameObject.GetComponentInChildren<NPC_Animation_Controller>().transform);
        _physicsManager = new NPC_PhysicsManager(GetComponent<NavMeshAgent>(), _npcData);
    }

    //Happens every time the NPC appears in the scene
    public void Initialize(NPC_Manager manager)
    {
        _majorManager = manager;

        Subscribe();
        EventBus.StartMovement();

        _ordered = false;
        _processingOrder = false;
    }

    private void Subscribe()
    {
        QueueManager.OnPositionChanged += SetQueueTarget;
    }

    public void ClearSubscriptions()
    {
        QueueManager.OnPositionChanged -= SetQueueTarget;
    }

    #endregion

    #region Target Management

    private void Update() => _physicsManager.Update();

    private void SetQueueTarget(NPC_Base npc, Transform target)
    {
        if (npc != this)
            return;

        SetDestination(target);
    }

    protected void SetDestination(Transform target) => _physicsManager.SetDestination(target);

    #endregion

    #region Public Methods

    public abstract void StartOrdering();
    public abstract void ReceiveOrder();
    public BaseNPC_Data_SO GetData() => _npcData;

    #endregion

    #region Private Methods

    protected virtual void OrderPlaced()
    {
        NPC_EventBus.PlaceOrder();

        _ordered = true;
        _processingOrder = false;
        Disable(physicsIncluded: false);
    }

    protected void Enable(bool physicsIncluded)
    {
        enabled = true;

        if (physicsIncluded)
            _physicsManager.EnablePhysics();
    }

    protected void Disable(bool physicsIncluded)
    {
        enabled = false;

        if (physicsIncluded)
            _physicsManager.DisablePhysics();
    }

    #region Customer Removal

    protected void RemoveCustomer(Transform target) => StartCoroutine(RemoveCoroutine(target));

    protected IEnumerator RemoveCoroutine(Transform target)
    {
        SetDestination(target);

        while (!HasTheRightDistance)
        {
            yield return null;
        }

        OnCustomerRemoved();
    }

    protected virtual void OnCustomerRemoved() => gameObject.SetActive(false);

    #endregion

    #endregion
}
