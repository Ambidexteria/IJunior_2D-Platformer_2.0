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

    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private Player _player;
    [SerializeField] private float _groundAttackDuration = 0.3f;
    [SerializeField] private float _hurtDuration = 0.2f;

    private Dictionary<State, IState> _states;
    private IState _currentStateValue;
    private State _currentStateType;
    private bool _isPaused;

    public bool IsGrounded => _groundDetector.IsGrounded;
    public Player Player => _player;

    private void Awake()
    {
        if (_groundDetector == null)
            throw new ArgumentNullException(nameof(_groundDetector) + " in " + nameof(PlayerStateController));

        if (_player == null)
            throw new ArgumentNullException(nameof(Player) + " in " + nameof(PlayerStateController));

        if (_groundAttackDuration <= 0)
            throw new ArgumentOutOfRangeException(nameof(_groundAttackDuration) + " in " + nameof(PlayerStateController));

        if (_hurtDuration <= 0)
            throw new ArgumentOutOfRangeException(nameof(_hurtDuration) + " in " + nameof(PlayerStateController));

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
        _currentStateValue.OnUpdate();

        if (_isPaused)
            return;

        if (_player.PlayerInput.IsAttacking && IsGrounded)
        {
            ChangeState(State.Attacking);
        }
        else if (_player.PlayerInput.Jumping)
        {
            ChangeState(State.Jumping);
        }
        else if (_player.PlayerInput.IsMoving && IsGrounded == false)
        {
            ChangeState(State.Jumping);
        }
        else if (_player.PlayerInput.IsMoving && IsGrounded)
        {
            if (_currentStateType != State.Walking)
                ChangeState(State.Walking);
        }
        else
        {
            if (_currentStateType != State.Idle)
                ChangeState(State.Idle);
        }
    }

    public void ChangeState(State newState)
    {
        if (_currentStateValue != null)
        {
            _currentStateValue.OnExit();
        }

        _currentStateType = newState;
        _currentStateValue = _states[newState];
        _currentStateValue.OnEnter();
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