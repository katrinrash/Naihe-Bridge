using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main manager for all NPC types, serving as the entry point for the NPC system and managing customer spawning, stage progression, 
/// queue integration, and completion tracking.
/// </summary>

public class NPC_Manager : MonoBehaviour
{
    [SerializeField] private NPC_DataManagement npcDataManagement;
    [SerializeField] private QueueManager queueManager;
    
    private NPC[] _customersPerLevel;
    private SpecialGuest _specialGuest;
    private int _index = 0;

    private int _customersPerStageCount;
    private int _spawnedCustomersCount = 0;
    private int _completedCustomers = 0;

    private float _spawnDelayInSeconds; 
    private float _currentDelay = 0;
    
    private IExecutable _currentExecutable;
    private NPC_Spawner _npcSpawner;

    #region Life Cycle

    private void Awake()
    {
        ShiftManager.OnShiftInitialized += InitializeManager;
        BaseStage.OnStageUpdate += SetUpStage;
        
        enabled = false;
    }
    
    private void OnDestroy()
    {
        ShiftManager.OnShiftInitialized -= InitializeManager;
        BaseStage.OnStageUpdate -= SetUpStage;
    }

    private void InitializeManager(ShiftData_SO data)
    {
        _npcSpawner = npcDataManagement.InitializeSpawner(data);

        _customersPerLevel = _npcSpawner.CreateCustomers();
        _specialGuest = _npcSpawner.CreateSpecialGuest();
        _index = 0;
    }

    private void Update()
    {
        if (_currentDelay >= _spawnDelayInSeconds)
        {
            SpawnNextCustomer();
            _currentDelay = 0;
        }
        else _currentDelay += Time.deltaTime;
    }

    #endregion

    private void SetUpStage(BaseStage stageManager)
    {
        _currentExecutable = stageManager;
        _customersPerStageCount = stageManager.CustomersPerStage;
        _spawnDelayInSeconds = stageManager.SpawnDelay;
        _spawnedCustomersCount = 0;
        _completedCustomers = 0;
        _currentDelay = 0;

        if (stageManager.CurrentStage == StagesEnum.Special)
        {
            _customersPerStageCount = 1;
            SetUpAfterSpawned(_specialGuest);
        }
        else
        {
            enabled = true;
            SpawnNextCustomer();
        }
    }

    private void SpawnNextCustomer()
    {
        if (++_spawnedCustomersCount >= _customersPerStageCount)
            enabled = false;

        SetUpAfterSpawned(_customersPerLevel[_index++]);
    }

    private void SetUpAfterSpawned(NPC_Base customer)
    {      
        customer.transform.position = transform.position;
        customer.gameObject.SetActive(true);
        customer.Initialize(this);
        queueManager.EnqueueCustomer(customer);
    }

    #region Methods for NPCs

    public (Transform, float) GetTableData() => npcDataManagement.GetTableData();
    public Transform GetEndPosition() => npcDataManagement.GetEndPosition();

    public void CustomerCompleted()
    { 
        if (++_completedCustomers >= _customersPerStageCount)
            _currentExecutable?.SetAsCompleted();
    }

    #endregion

}

