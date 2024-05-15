using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Medkit : MonoBehaviour
{
    [SerializeField] private float _healthRestoreAmount;

    public float HealthRestoreAmount => _healthRestoreAmount;
}