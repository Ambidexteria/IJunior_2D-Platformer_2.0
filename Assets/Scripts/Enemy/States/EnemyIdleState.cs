using System;

public class EnemyIdleState : IState
{
    private readonly string Idle = nameof(Idle);

    private EnemyStateController _controller;

    public EnemyIdleState(EnemyStateController stateController)
    {
        if (stateController == null)
            throw new ArgumentNullException(nameof(stateController) + " in " + nameof(EnemyIdleState));

        _controller = stateController;
    }

    public void OnEnter()
    {
        _controller.Enemy.Animator.Play(Idle);
    }

    public void OnExit() { }

    public void OnUpdate() { }
}