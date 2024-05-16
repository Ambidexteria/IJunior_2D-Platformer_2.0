using System;
using UnityEngine;

public class EnemyPatrollingState : IState
{
    private readonly string Walk = nameof(Walk);

    private EnemyStateController _controller;
    private PatrolZone _patrolZone;
    private Vector2 _movementDirection;

    public EnemyPatrollingState(EnemyStateController stateController, PatrolZone patrolZone)
    {
        if(stateController == null)
            throw new ArgumentNullException(nameof(stateController));

        if(patrolZone == null)
            throw new ArgumentNullException(nameof(patrolZone));

        _patrolZone = patrolZone;
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
        if (_controller.Enemy.PositionX >= _patrolZone.RightEdge)
        {
            _movementDirection = Vector2.left;
        }
        else if (_controller.Enemy.PositionX <= _patrolZone.LeftEdge)
        {
            _movementDirection = Vector2.right;
        }
    }
}