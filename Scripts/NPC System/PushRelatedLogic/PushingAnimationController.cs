using System.Collections;
using UnityEngine;

/// <summary>
/// Manages the animation and customer elimination process when the cooking time limit is exceeded.
/// </summary>

public class PushingAnimationController : MonoBehaviour 
{
    [Header("Destination Data")]
    [SerializeField] private Transform pushPoint;
    [SerializeField] private Transform deathPoint;

    [Header("Animation Data")]
    [SerializeField] private AnimationCurve curvePickUp;
    [SerializeField] private AnimationCurve curveThrow;

    [Header("Duration Data")]
    [SerializeField] private float durationPickUp;
    [SerializeField] private float durationThrow;

    private PushingManager _pushingManager;
    private QueueManager _queueManager;
    private NPC _victim;
    private NPC _killer;
    private Transform _targetPoint;

    public void Initialize(NPC victim, NPC killer, QueueManager queueManager, PushingManager manager)
    {
        _victim = victim;
        _killer = killer;
        _targetPoint = _killer.GetSpecificData();
        _queueManager = queueManager;
        _pushingManager = manager;
    }

    public void PickUp()
    {
        StartCoroutine(PickUpCustomer());
    }

    public void Eliminate()
    {
        StartCoroutine(EliminateCustomer());
    }


    private IEnumerator PickUpCustomer()
    {
        _victim.transform.GetPositionAndRotation(out var customerStart, out var rotationStart);

        var elapsed = 0f;
        while (elapsed < durationPickUp)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / durationPickUp;
            _victim.transform.SetPositionAndRotation(Vector3.Lerp(customerStart, _targetPoint.position, curvePickUp.Evaluate(t)), Quaternion.Slerp(rotationStart, _targetPoint.rotation, curvePickUp.Evaluate(t)));
            yield return null;
        }

        _victim.transform.SetParent(_killer.GetComponentInChildren<NPC_Animation_Controller>().transform);
        _killer.UpdateMovement(pushPoint);
        _pushingManager.OnPreparationCompleted();
    }

    private IEnumerator EliminateCustomer() 
    {
        _victim.transform.SetParent(null);
        AudioManager.Instance.PlayNPCPush();

        Vector3 start = _victim.transform.position;
        Vector3 end = deathPoint.position;

        float elapsed = 0f;

        NPC_EventBus.FinishOrder();
        InputManager.Instance.InteractionAction.Enable();

        while (elapsed < durationThrow)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / durationThrow;

            Vector3 pos = Vector3.Lerp(start, end, t);

            float height = curveThrow.Evaluate(t);
            pos.y += height;

            _victim.transform.position = pos;

            yield return null;
        }

        _victim.gameObject.SetActive(false);
        AudioManager.Instance.PlayBoiling();
    }

}
