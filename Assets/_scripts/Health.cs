using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxValue;

    public event Action Dying;

    public float Current { get; private set; }

    private void Awake()
    {
        Current = _maxValue;
    }

    public void Increase(float amount)
    {
        if(amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount) + " in " + nameof(Health));

        Current += amount;
        Current = Mathf.Clamp(Current, 0, _maxValue);

        Debug.Log(transform.root.name + " " + nameof(Health) + " " + nameof(Increase) + " " + amount);
    }

    public void Decrease(float amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount) + " in " + nameof(Health));

        Current -= amount;

        if (Current <= 0)
            Dying?.Invoke();

        Debug.Log(transform.root.name + " " + nameof(Health) + " " + nameof(Decrease) + " " + amount);
    }
}