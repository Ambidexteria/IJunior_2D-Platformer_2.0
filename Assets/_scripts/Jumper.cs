using System;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 20;
    [SerializeField] private Rigidbody2D _rigidbody;

    private void Awake()
    {
        if (_rigidbody == null)
            throw new ArgumentNullException(nameof(_rigidbody) + " " + nameof(Jumper));
    }

    public void Jump()
    {
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
}
