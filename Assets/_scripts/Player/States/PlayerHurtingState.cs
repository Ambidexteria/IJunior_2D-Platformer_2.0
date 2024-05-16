using System;
using UnityEngine;

public class PlayerHurtingState : IState
{
    private readonly string Hurt = nameof(Hurt);

    private PlayerStateController _stateController;
    private float _duration;
    private float _resumeTime;

    public PlayerHurtingState(PlayerStateController stateController, float duration)
    {
        if (stateController == null)
            throw new ArgumentNullException(nameof(stateController));

        if(duration <= 0)
            throw new ArgumentOutOfRangeException(nameof(duration));

        _stateController = stateController;
        _duration = duration;
    }

    public void OnEnter()
    {
        _stateController.Pause();
        _stateController.Player.Animator.Play(Hurt);
        _resumeTime = Time.time + _duration;
    }

    public void OnUpdate()
    {
        if(Time.time > _resumeTime)
        {
            _stateController.Resume();
            _stateController.ChangeState(PlayerStateController.State.Idle);
        }
    }

    public void OnExit() { }
}