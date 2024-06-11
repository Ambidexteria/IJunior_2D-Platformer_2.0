using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyWeapon _weapon;
    [SerializeField] private Health _health;
    [SerializeField] private Animator _animator;
    [SerializeField] private Mover _mover;
    [SerializeField] private Rotator _rotator;

    private Rigidbody2D _rigidbody;
    private CapsuleCollider2D _collider;

    public event Action Hurt;
    public event Action Died;

    public EnemyWeapon Weapon => _weapon;
    public Animator Animator => _animator;
    public float PositionX => transform.position.x;
    public float HealthCurrent => _health.Current;

    private void Awake()
    {
        if(_weapon == null)
            throw new ArgumentNullException(nameof(_weapon) + " in " + nameof(Enemy));

        if(_animator == null)
            throw new ArgumentNullException(nameof(_animator) + " in " + nameof(Enemy));

        if(_health == null)
            throw new ArgumentNullException(nameof(_health) + " in " + nameof(Enemy));

        if(_mover == null)
            throw new ArgumentNullException(nameof(_mover) + " in " + nameof(Enemy));

        if(_rotator == null)
            throw new ArgumentNullException(nameof(_rotator) + " in " + nameof(Enemy));

        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
    }

    private void OnEnable()
    {
        _health.Dying += Die;
    }

    private void OnDisable()
    {
        _health.Dying -= Die;
    }

    public void Move(Vector2 direction)
    {
        _mover.Move(direction);
        RotateToMoveDirection(direction);
    }

    public void Attack()
    {
        _weapon.AttackRegular();
    }

    public void TakeDamage(float amount)
    {
        _health.Decrease(amount);

        Hurt?.Invoke();
    }

    private void Die()
    {
        _rigidbody.bodyType = RigidbodyType2D.Static;
        _collider.enabled = false;
        _rotator.enabled = false;
        _mover.enabled = false;
        _weapon.gameObject.SetActive(false);
        _health.gameObject.SetActive(false);

        Died?.Invoke();
    }

    private void RotateToMoveDirection(Vector2 direction)
    {
        if(direction == Vector2.left)
            _rotator.LookLeft();
        else if(direction == Vector2.right)
            _rotator.LookRight();
    }
}