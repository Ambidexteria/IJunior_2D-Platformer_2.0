using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    public enum State
    {
        Walking,
        Idle,
        Jumping,
        Attacking,
        Hurting,
        Died
    }

    [SerializeField] private Player _player;
    [SerializeField] private float _groundAttackDuration = 0.3f;
    [SerializeField] private float _hurtDuration;

    private Dictionary<State, IState> _states;
    private IState _currentState;
    private bool _isPaused;

    public Player Player => _player;

    private void Awake()
    {
        if (_player == null)
            throw new ArgumentNullException(nameof(Player));

        if(_groundAttackDuration <= 0)
            throw new ArgumentOutOfRangeException(nameof(_groundAttackDuration));

        if(_hurtDuration <= 0)
            throw new ArgumentOutOfRangeException(nameof(_hurtDuration));

        InitializeStates();
        ChangeState(State.Idle);
    }

    private void OnEnable()
    {
        _player.Hurt += ChangeHurtState;
        _player.PlayerEvents.Died += ChangeDiedState;
    }

    private void OnDisable()
    {
        _player.Hurt -= ChangeHurtState;
        _player.PlayerEvents.Died -= ChangeDiedState;
    }

    private void Update()
    {
        _currentState.OnUpdate();

        if (_isPaused)
            return;

        if(_player.PlayerInput.IsAttacking && _player.IsGrounded)
        {
            ChangeState(State.Attacking);
        }
        else if (_player.PlayerInput.Jump)
        {
            ChangeState(State.Jumping);
        }
        else if (_player.PlayerInput.IsMoving && _player.IsGrounded == false)
        {
            ChangeState(State.Jumping);
        }
        else if (_player.PlayerInput.IsMoving && _player.IsGrounded)
        {
            ChangeState(State.Walking);
        }
        else
        {
            ChangeState(State.Idle);
        }
    }

    public void ChangeState(State newState)
    {
        if (_currentState != null)
        {
            _currentState.OnExit();
        }

        _currentState = _states[newState];
        _currentState.OnEnter();
    }

    public void Pause()
    {
        _isPaused = true;
    }

    public void Resume()
    {
        _isPaused = false;
    }

    private void ChangeHurtState()
    {
        ChangeState(State.Hurting);
    }

    private void ChangeDiedState()
    {
        ChangeState(State.Died);
    }

    private void InitializeStates()
    {
        _states = new Dictionary<State, IState>();

        _states.Add(State.Idle, new PlayerIdleState(this));
        _states.Add(State.Walking, new PlayerWalkingState(this));
        _states.Add(State.Jumping, new PlayerJumpingState(this));
        _states.Add(State.Attacking, new PlayerAttackingState(this, _groundAttackDuration));
        _states.Add(State.Hurting, new PlayerHurtingState(this, _hurtDuration));
        _states.Add(State.Died, new PlayerDiedState(this));
    }
}
