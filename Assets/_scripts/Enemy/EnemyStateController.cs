using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    public enum States
    {
        Idle,
        Patrolling,
        Chasing,
        Attacking,
        Hurting,
        Dying
    }

    [SerializeField] private Enemy _enemy;
    [SerializeField] private PlayerEvents _playerEvents;
    [SerializeField] private PlayerDetector _playerVisionDetector;
    [SerializeField] private PlayerDetector _playerAttackRangeDetector;
    [SerializeField] private PatrolZone _patrolZone;
    [SerializeField] private float _hurtStateDuration = 0.5f;
    [SerializeField] private float _attackStateDuration = 1f;

    private Dictionary<States, IState> _states;
    private IState _currentStateValue;
    private States _currentStateType;
    private bool _isPaused = false;

    public Enemy Enemy => _enemy;
    public bool PlayerInAttackRange => _playerAttackRangeDetector.Detected;
    public bool PlayerSpotted => _playerVisionDetector.Detected;

    private void Awake()
    {
        if (_enemy == null)
            throw new ArgumentNullException(nameof(Enemy) + " in " + nameof(EnemyStateController));

        if (_playerVisionDetector == null)
            throw new ArgumentNullException(nameof(_playerVisionDetector) + " in " + nameof(EnemyStateController));

        if (_playerAttackRangeDetector == null)
            throw new ArgumentNullException(nameof(_playerAttackRangeDetector) + " in " + nameof(EnemyStateController));

        if (_patrolZone == null)
            throw new ArgumentNullException(nameof(PatrolZone) + " in " + nameof(name));

        InitializeStates();
    }

    private void Start()
    {
        ChangeState(States.Patrolling);
    }

    private void OnEnable()
    {
        _enemy.Hurt += ChangeHurtState;
        _enemy.Died += DisableControllerEnemyDied;

        if (_playerEvents != null)
            _playerEvents.Died += DisableControllerPlayerDied;
    }

    private void OnDisable()
    {
        _enemy.Hurt -= ChangeHurtState;
        _enemy.Died -= DisableControllerEnemyDied;

        if (_playerEvents != null)
            _playerEvents.Died -= DisableControllerPlayerDied;
    }

    private void Update()
    {
        _currentStateValue.OnUpdate();

        if (_isPaused)
            return;

        if (PlayerSpotted && !PlayerInAttackRange)
        {
            if (_currentStateType != States.Chasing)
                ChangeState(States.Chasing);
        }
        else if (PlayerInAttackRange)
        {
            ChangeState(States.Attacking);
        }
        else
        {
            if (_patrolZone != null)
            {
                if (_currentStateType != States.Patrolling)
                    ChangeState(States.Patrolling);
            }
            else
            {
                ChangeState(States.Idle);
            }
        }
    }

    public void ChangeState(States newState)
    {
        if (_currentStateValue != null)
            _currentStateValue.OnExit();

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

    public bool TryGetPlayerPosition(out Vector2 position)
    {
        return _playerVisionDetector.TryGetPlayerPosition(out position);
    }

    public void SetPatrolZone(PatrolZone zone)
    {
        _patrolZone = zone;
    }

    private void ChangeHurtState()
    {
        ChangeState(States.Hurting);
    }

    private void DisableControllerPlayerDied()
    {
        Pause();
        ChangeState(States.Idle);
        enabled = false;
    }

    private void DisableControllerEnemyDied()
    {
        Pause();
        ChangeState(States.Dying);
        enabled = false;
    }

    private void InitializeStates()
    {
        _states = new Dictionary<States, IState>();

        _states.Add(States.Idle, new EnemyIdleState(this));
        _states.Add(States.Patrolling, new EnemyPatrollingState(this, _patrolZone));
        _states.Add(States.Attacking, new EnemyAttackingState(this, _attackStateDuration));
        _states.Add(States.Hurting, new EnemyHurtingState(this, _hurtStateDuration));
        _states.Add(States.Chasing, new EnemyChasingState(this));
    }
}