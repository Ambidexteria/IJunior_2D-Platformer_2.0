using System;
using UnityEngine;

public class EnemyAttackingState : IState
{
    private readonly string Attack = nameof(Attack);

    private EnemyStateController _controller;
    private float _duration;
    private float _resumeTime;

    public EnemyAttackingState(EnemyStateController stateController, float duration)
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
        _controller.Enemy.Animator.Play(Attack);
        _resumeTime = Time.time + _duration;
        _controller.Enemy.Attack();
    }

    public void OnExit()
    {
        _controller.Enemy.Animator.Rebind();
        _controller.Enemy.Weapon.ResetRegularAttack();
    }

    public void OnUpdate()
    {
        if (Time.time > _resumeTime)
            _controller.Resume();
    }
}