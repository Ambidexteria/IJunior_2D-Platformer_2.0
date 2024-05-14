using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyWeapon _weapon;
    [SerializeField] private Animator _animator;

    public event Action Hurt;

    public EnemyWeapon Weapon => _weapon;
    public Animator Animator => _animator;
    public float PositionX => transform.position.x;

    public void Move(Vector2 direction)
    {
        
    }

    public void Attack()
    {

    }
}
