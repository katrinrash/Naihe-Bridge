using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Helper class used by NPC_Base to manage shared movement and physics behaviour for all NPC types.
/// </summary>

public class NPC_PhysicsManager
{
    // Static Data
    private readonly NavMeshAgent _navMeshAgent;
    private readonly float _stopOffset;
    private readonly float _rotationSpeed;

    // Dynamic Data
    private Transform _currentTarget;

    public bool HasTheRightDistance => _currentTarget != null && Vector3.Distance(_navMeshAgent.transform.position, _currentTarget.position) <= _stopOffset;

    public NPC_PhysicsManager(NavMeshAgent navMeshAgent, BaseNPC_Data_SO data)
    {
        _navMeshAgent = navMeshAgent;

        _stopOffset = data.StopOffset;
        _rotationSpeed = data.RotationSpeed;

        _navMeshAgent.speed = data.GetSpeed();
        _navMeshAgent.updateRotation = false;
    }

    #region LifeCycle 

    public void Update()
    {
        if (_navMeshAgent.pathPending)
            return;

        if (!HasTheRightDistance)
        {
            RotateWhileMoving();
        }
        else
        {
            RotateToTarget();
        }
    }

    public void SetDestination(Transform target)
    {
        if (target == _currentTarget)
            return;
        
        _currentTarget = target;
        _navMeshAgent.SetDestination(_currentTarget.position);
    }

    public void EnablePhysics() => _navMeshAgent.enabled = true;
    public void DisablePhysics() => _navMeshAgent.enabled = false;

    #endregion

    #region Rotation

    private void RotateWhileMoving()
    {
        Vector3 velocity = _navMeshAgent.velocity;
        velocity.y = 0;

        if (velocity.sqrMagnitude < 0.01f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(velocity);
        _navMeshAgent.transform.rotation = Quaternion.Slerp(_navMeshAgent.transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    private void RotateToTarget()
    {
        Quaternion targetRotation = _currentTarget.rotation;
        _navMeshAgent.transform.rotation = Quaternion.Slerp(_navMeshAgent.transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    #endregion
}
