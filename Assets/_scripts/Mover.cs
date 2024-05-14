using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;

    public void Move(Vector2 direction)
    {
        transform.Translate(_speed * Time.deltaTime *  direction, Space.World);
    }
}
