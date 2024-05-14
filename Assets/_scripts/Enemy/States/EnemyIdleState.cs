using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IState
{
    private readonly string Idle = nameof(Idle);

    private EnemyStateController _controller;

    public EnemyIdleState(EnemyStateController stateController)
    {
        _controller = stateController;
    }

    public void OnEnter()
    {
        _controller.Enemy.Animator.Play(Idle);
    }

    public void OnExit()
    {
        _controller.Enemy.Animator.Rebind();
    }

    public void OnUpdate()
    {
    }
}
