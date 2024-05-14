using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    public bool IsMoving { get; private set; }
    public bool Jump { get; private set; }
    public bool IsAttacking { get; private set; }
    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxisRaw(Horizontal);
        IsMoving = Direction != 0;
        Jump = Input.GetKeyDown(KeyCode.Space);
        IsAttacking = Input.GetKeyDown(KeyCode.E);
    }
}
