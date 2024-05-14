using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtingState : IState
{
    private readonly string Hurt = nameof(Hurt);

    private EnemyStateController _controller;
    private float _duration;
    private float _resumeTime;

    public EnemyHurtingState(EnemyStateController stateController, float duration)
    {
        if (stateController == null)
            throw new ArgumentNullException(nameof(stateController));

        if (duration <= 0)
            throw new ArgumentOutOfRangeException(nameof(duration));

        _controller = stateController;
        _duration = duration;
    }

    public void OnEnter()
    {
        _controller.Pause();
        _controller.Enemy.Animator.Play(Hurt);
        _resumeTime = Time.time + _duration;
    }

    public void OnExit()
    {
        _controller.Enemy.Animator.Rebind();
    }

    public void OnUpdate()
    {
        if (Time.time > _resumeTime)
        {
            _controller.Resume();
            _controller.ChangeState(EnemyStateController.States.Patrolling);
        }
    }
}
