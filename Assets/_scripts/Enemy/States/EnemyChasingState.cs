using System;
using UnityEngine;

public class EnemyChasingState : IState
{
    private readonly string Walk = nameof(Walk);

    private EnemyStateController _controller;
    private Vector2 _movementDirection;

    public EnemyChasingState(EnemyStateController stateController)
    {
        if (stateController == null)
            throw new ArgumentNullException(nameof(stateController));

        _controller = stateController;
    }

    public void OnEnter()
    {
        _controller.Enemy.Animator.Play(Walk);
        _movementDirection = Vector2.left;
    }

    public void OnUpdate()
    {
        ChangeMovementDirection();
        _controller.Enemy.Move(_movementDirection);
    }

    public void OnExit() { }

    private void ChangeMovementDirection()
    {
        if (_controller.TryGetPlayerPosition(out Vector2 position))
        {
            if (_controller.Enemy.PositionX >= position.x)
            {
                _movementDirection = Vector2.left;
            }
            else if (_controller.Enemy.PositionX <= position.x)
            {
                _movementDirection = Vector2.right;
            }
        }
    }
}