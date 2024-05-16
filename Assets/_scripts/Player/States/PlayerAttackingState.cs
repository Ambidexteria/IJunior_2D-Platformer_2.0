using System;
using UnityEngine;

public class PlayerAttackingState : IState
{
    private readonly string Attack = nameof(Attack);

    private PlayerStateController _stateController;
    private float _attackDuration;
    private float _resumeTine;

    public PlayerAttackingState(PlayerStateController stateController, float attackDuration)
    {
        if (stateController == null)
            throw new ArgumentNullException(nameof(stateController));

        if (attackDuration <= 0)
            throw new ArgumentOutOfRangeException(nameof(attackDuration));

        _attackDuration = attackDuration;
        _stateController = stateController;
    }

    public void OnEnter()
    {
        _stateController.Pause();
        _resumeTine = Time.time + _attackDuration;
        _stateController.Player.Animator.Play(Attack);
        _stateController.Player.AttackOnGround();
    }

    public void OnExit()
    {
        _stateController.Player.Weapon.ResetRegularAttack();
    }

    public void OnUpdate()
    {
        if (Time.time > _resumeTine)
        {
            _stateController.Resume();
            _stateController.ChangeState(PlayerStateController.State.Idle);
        }
    }
}