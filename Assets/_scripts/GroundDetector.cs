using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GroundDetector : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;

    public bool IsGrounded { get; private set; } = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ground ground))
            IsGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ground ground))
            IsGrounded = false;
    }
}
