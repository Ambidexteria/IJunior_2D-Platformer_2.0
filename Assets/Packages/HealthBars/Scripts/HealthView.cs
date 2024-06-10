using System;
using UnityEngine;

public abstract class HealthView : MonoBehaviour
{
    [SerializeField] private Health _health;

    private void Awake()
    {
        if (_health == null)
            throw new ArgumentNullException();
    }

    private void Start()
    {
        Display(_health.Current);        
    }

    private void OnEnable()
    {
        _health.Changed += Display;
        _health.Dying += Deactivate;
    }

    private void OnDisable()
    {
        _health.Changed -= Display;
        _health.Dying -= Deactivate;
    }

    public float GetMaxHealth()
    {
        return _health.MaxValue;
    }

    public abstract void Display(float value);

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}