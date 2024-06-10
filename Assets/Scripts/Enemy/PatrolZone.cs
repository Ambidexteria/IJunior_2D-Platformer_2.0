using System;
using System.Collections.Generic;
using UnityEngine;

public class PatrolZone : MonoBehaviour
{
    [SerializeField] private float _patrolDistance = 3f;

    public float LeftEdge { get; private set; }
    public float RightEdge { get; private set; }

    private void Awake()
    {
        if (_patrolDistance <= 0)
            throw new ArgumentOutOfRangeException(nameof(_patrolDistance) + " in " + nameof(name));

        CalculateEndPoints();
    }

    private void CalculateEndPoints()
    {
        LeftEdge = transform.position.x - _patrolDistance;
        RightEdge = transform.position.x + _patrolDistance;
    }
}