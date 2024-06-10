using System;

public class EnemyDyingState : IState
{
    private readonly string Die = nameof(Die);

    private EnemyStateController _controller;

    public EnemyDyingState(EnemyStateController stateController)
    {
        if (stateController == null)
            throw new ArgumentNullException(nameof(stateController));

        _controller = stateController;
    }

    public void OnEnter()
    {
        _controller.Enemy.Animator.Play(Die);
    }

    public void OnExit() { }

    public void OnUpdate() { }
}