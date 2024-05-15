using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CoinPicker : MonoBehaviour
{
    [SerializeField] private int _coinsPicked = 0;

    public int CoinsPicked
    {
        get
        {
            return _coinsPicked;
        }
        private set
        {
            _coinsPicked = Mathf.Clamp(value, 0, int.MaxValue);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
            CoinsPicked++;
    }
}
