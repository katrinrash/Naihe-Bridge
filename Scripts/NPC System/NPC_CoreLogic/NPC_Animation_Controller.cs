using DG.Tweening;
using UnityEngine;

/// <summary>
/// Manages data-driven movement animations for NPC, including regular customers and special guests, using Scriptable Object settings and DOTween.
/// </summary>

public class NPC_Animation_Controller : MonoBehaviour
{
    [SerializeField] private NPC_Animation_Data_SO data_SO;
    [SerializeField] private float yOffset;

    private NPC_Base _tiedNPC;
    private Tween _animationTween;
    private Vector3 _startV;

    #region Life Cycle

    public void Awake()
    {
        _tiedNPC = GetComponentInParent<NPC_Base>();

        SetStartPosition();
    }

    private void SetStartPosition()
    {
        _startV = transform.localPosition;
        _startV.y = yOffset;
        transform.localPosition = _startV;
    }

    private void OnEnable()
    {
        _tiedNPC.EventBus.OnMovementStarted += Play;
        _tiedNPC.EventBus.OnMovementStopped += Stop;
    }

    private void OnDisable()
    {
        _tiedNPC.EventBus.OnMovementStarted -= Play;
        _tiedNPC.EventBus.OnMovementStopped -= Stop;

        Stop();
        ClearAnimation();
    }

    #endregion

    #region Animation Cycle

    private void Play()
    {
        if(_animationTween == null)
            CreateAnimation();

        _animationTween.Play();
    }
    private void Stop() => _animationTween?.Rewind();

    private void CreateAnimation()
    {
        float intensity = data_SO.GetRandomHeight();
        float duration = data_SO.GetRandomDuration();

        _animationTween = transform
            .DOLocalMoveY(_startV.y + intensity, duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .SetAutoKill(false);
    }
    private void ClearAnimation()
    {
        _animationTween?.Kill();
        _animationTween = null;
    }

    #endregion
}
