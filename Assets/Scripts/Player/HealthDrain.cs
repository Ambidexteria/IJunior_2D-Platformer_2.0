using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealthDrain : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;
    [SerializeField] private float _duration = 6f;
    [SerializeField] private float _cooldown = 20f;
    [SerializeField] private float _damagePerSecond = 5f;

    private Enemy _target;
    private float _reloadTime;
    private float _skillEndTime;
    private Coroutine _drainCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy) && _target == null)
            _target = enemy;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy) && _target == enemy)
            _target = null;
    }

    public bool TryUse()
    {
        if (_reloadTime < Time.time && _target != null)
        {
            if (_drainCoroutine != null)
                StopCoroutine(_drainCoroutine);

            _drainCoroutine = StartCoroutine(Drain());
            _reloadTime = Time.time + _cooldown;

            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator Drain()
    {
        _skillEndTime = Time.time + _duration;

        while (_skillEndTime > Time.time && _target != null)
        {
            if (_target.HealthCurrent <= 0)
                yield break;

            float drainedHealth = _damagePerSecond * Time.deltaTime;
            _playerHealth.Increase(drainedHealth);
            _target.TakeDamage(drainedHealth);

            yield return null;
        }
    }
}
