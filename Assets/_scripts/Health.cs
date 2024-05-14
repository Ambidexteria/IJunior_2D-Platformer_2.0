using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxValue;

    public event Action Dying;

    public float Current { get; private set; }

    public void Increase(float amount)
    {
        if(amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount) + " in " + nameof(Health));

        Current += amount;
        Current = Mathf.Clamp(Current, 0, _maxValue);
    }

    public void Decrease(float amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount) + " in " + nameof(Health));
        
        Current -= amount;

        if (Current <= 0)
            Dying?.Invoke();
    }
}
