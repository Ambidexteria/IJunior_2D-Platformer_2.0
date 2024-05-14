using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private List<Coin> _coins;

    private void Awake()
    {
        if(_coinPrefab == null)
            throw new System.ArgumentNullException(nameof(_coinPrefab) + " in " + nameof(CoinSpawner));

        if (_spawnPoints != null)
            SpawnCoins();
    }

    private void OnEnable()
    {
        if (_coins == null)
            return;

        foreach (Coin coin in _coins)
            coin.Picked += Delete;
    }

    private void OnDisable()
    {
        if (_coins == null)
            return;

        foreach (Coin coin in _coins)
            coin.Picked -= Delete;
    }

    public void Spawn(Vector3 position)
    {
        Coin coin = Instantiate(_coinPrefab, position, Quaternion.identity);
        _coins.Add(coin);
        coin.Picked += Delete;
    }

    private void SpawnCoins()
    {
        if(_spawnPoints.Length == 0)
            return;

        foreach(var spawnPoint in _spawnPoints)
            Spawn(spawnPoint.position);
    }

    private void Delete(Coin coin)
    {
        Destroy(coin.gameObject);
    }
}
