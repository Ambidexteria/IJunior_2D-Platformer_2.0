using System;
using UnityEngine;

public class PlayerJumpingState : IState
{
    private readonly string Jump = nameof(Jump);

    private PlayerStateController _stateController;

    public PlayerJumpingState(PlayerStateController stateController)
    {
        if (stateController == null)
            throw new ArgumentNullException(nameof(stateController));

        _stateController = stateController;
    }

    public void OnEnter()
    {
        _stateController.Player.Animator.Play(Jump);

        if (_stateController.Player.IsGrounded)
        {
            Debug.Log(nameof(PlayerJumpingState));
            _stateController.Player.Jump();
        }
    }

    public void OnUpdate()
    {
        _stateController.Player.Move();
    }

    public void OnExit()
    {
    }
}
