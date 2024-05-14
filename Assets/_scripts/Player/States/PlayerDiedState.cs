using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDiedState : IState
{
    private readonly string Die = nameof(Die);

    private PlayerStateController _stateController;

    public PlayerDiedState(PlayerStateController stateController)
    {
        if (stateController == null)
            throw new ArgumentNullException(nameof(stateController));

        _stateController = stateController;
    }

    public void OnEnter()
    {
        _stateController.Pause();
        _stateController.Player.Animator.Play(Die);
    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {
        
    }
}
