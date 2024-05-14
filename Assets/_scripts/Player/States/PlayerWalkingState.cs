using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingState : IState
{
    private readonly string Walk = nameof(Walk);

    private PlayerStateController _stateController;

    public PlayerWalkingState(PlayerStateController stateController)
    {
        if (stateController == null)
            throw new ArgumentNullException(nameof(stateController));

        _stateController = stateController;
    }

    public void OnEnter()
    {
        _stateController.Player.Animator.Play(Walk);
    }

    public void OnUpdate()
    {
        _stateController.Player.Move();
    }

    public void OnExit()
    {

    }
}
