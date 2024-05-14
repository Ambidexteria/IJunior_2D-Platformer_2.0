using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyWeapon _weapon;
    [SerializeField] private Health _health;
    [SerializeField] private Animator _animator;
    [SerializeField] private Mover _mover;
    [SerializeField] private Rotator _rotator;

    public event Action Hurt;
    public event Action Died;

    public EnemyWeapon Weapon => _weapon;
    public Animator Animator => _animator;
    public float PositionX => transform.position.x;

    private void Awake()
    {
        
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
