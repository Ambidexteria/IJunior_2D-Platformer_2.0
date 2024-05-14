using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerDetector : MonoBehaviour
{
    private Player _player;

    public bool Detected => _player != null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _player = player;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
        {
            _player = null;
        }
    }

    public bool TryGetPlayerPosition(out Vector2 position)
    {
        position = Vector2.zero;

        if (_player == null)
        {
            return false;
        }
        else
        {
            position = _player.transform.position;
            return true;
        }
    }
}
