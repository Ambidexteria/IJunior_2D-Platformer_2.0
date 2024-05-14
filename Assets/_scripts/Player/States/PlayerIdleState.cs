using System;

public class PlayerIdleState : IState
{
    private readonly string Idle = nameof(Idle);

    private PlayerStateController _stateController;

    public PlayerIdleState(PlayerStateController stateController)
    {
        if (stateController == null)
            throw new ArgumentNullException(nameof(stateController));

        _stateController = stateController;
    }

    public void OnEnter()
    {
        _stateController.Player.Animator.Play(Idle);
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {

    }
}
