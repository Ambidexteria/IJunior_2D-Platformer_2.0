using UnityEngine;

public class Rotator : MonoBehaviour
{
    private Vector3 _flipY = new Vector3(0, 180, 0);

    public void FlipY()
    {
        transform.Rotate(_flipY);
    }

    public void LookRight()
    {
        transform.right = Vector2.right;
    }

    public void LookLeft()
    {
        transform.right = Vector3.left;
    }
}
