using System;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Collider2D _attackZone;
    [SerializeField] private float _regularAttackDamage;

    private Enemy _target;

    private void Awake()
    {
        if (_attackZone == null)
            throw new ArgumentNullException(nameof(_attackZone) + " in " + nameof(PlayerWeapon));

        _attackZone.isTrigger = true;
        _attackZone.enabled = false;
    }

    public void AttackRegular()
    {
        _attackZone.enabled = true;
    }

    public void ResetRegularAttack()
    {
        _attackZone.enabled = false;
        _target = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy target) && _target != target)
        {
            _target = target;
            target.TakeDamage(_regularAttackDamage);
        }
    }
}
