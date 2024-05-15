using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private Collider2D _attackZone;
    [SerializeField] private float _regularAttackDamage;

    private Player _player;

    private void Awake()
    {
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
        _player = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player target) && _player != target)
        {
            _player = target;
            target.TakeDamage(_regularAttackDamage);
        }
    }
}
