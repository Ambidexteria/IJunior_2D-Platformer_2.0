using System;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public event Action Died;

    public void InvokeDiedEvent()
    {
        Died?.Invoke();
    }
}
