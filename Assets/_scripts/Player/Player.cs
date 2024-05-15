using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IDamageable
{
    private const string Horizontal = nameof(Horizontal);

    [SerializeField] private PlayerInput _input;
    [SerializeField] PlayerEvents _playerEvents;
    [SerializeField] private Health _health;
    [SerializeField] private PlayerWeapon _weapon;
    [SerializeField] private Mover _mover;
    [SerializeField] private Rotator _rotator;
    [SerializeField] private Jumper _jumper;
    [SerializeField] private Animator _animator;

    public event Action Hurt;

    public Animator Animator => _animator;
    public PlayerInput PlayerInput => _input;
    public PlayerEvents PlayerEvents => _playerEvents;
    public PlayerWeapon Weapon => _weapon;

    private void Awake()
    {
        if (_input == null)
            throw new ArgumentNullException(nameof(PlayerInput) + " in " + nameof(name));

        if (_playerEvents == null)
            throw new ArgumentNullException(nameof(PlayerEvents) + " in " + nameof(PlayerStateController));

        if (_health == null)
            throw new ArgumentNullException(nameof(Health) + " in " + nameof(name));

        if (_mover == null)
            throw new ArgumentNullException(nameof(Mover) + " in " + nameof(name));

        if (_rotator == null)
            throw new ArgumentNullException(nameof(Rotator) + " in " + nameof(name));

        if (_jumper == null)
            throw new ArgumentNullException(nameof(Jumper) + " in " + nameof(name));

        if (_animator == null)
            throw new ArgumentNullException(nameof(Animator) + " in " + nameof(name));
    }

    private void OnEnable()
    {
        _health.Dying += Die;
    }

    private void OnDisable()
    {
        _health.Dying -= Die;
    }

    private void Update()
    {
        ChangeDirection();
    }

    public void Move()
    {
        _mover.Move(transform.right);
    }

    public void Jump()
    {
        _jumper.Jump();
    }

    public void AttackOnGround()
    {
        _weapon.AttackRegular();
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        Hurt?.Invoke();
        _health.Decrease(damage);
    }

    private void Die()
    {
        _input.enabled = false;
        _weapon.enabled = false;
        _mover.enabled = false;
        _rotator.enabled = false;

        _playerEvents.InvokeDiedEvent();
    }

    private void ChangeDirection()
    {
        float direction = PlayerInput.Direction;

        if (direction < 0)
            _rotator.LookLeft();
        else if (direction > 0)
            _rotator.LookRight();
    }
}