using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private CoinDetector _coinDetector;
    [SerializeField] private Coin _coin;
    [SerializeField] private Transform[] _spawnPosition;

    private void Awake()
    {
        if (_coin != null && _spawnPosition != null && _spawnPosition.Length > 0)
            Spawn();
    }

    private void OnEnable()
    {
        _coinDetector.CoinDetected += DestroyCoin;
    }

    private void OnDisable()
    {
        _coinDetector.CoinDetected -= DestroyCoin;
    }

    private void Spawn()
    {
        for (int i = 0; i < _spawnPosition.Length; i++)
            Instantiate(_coin, _spawnPosition[i].transform.position, Quaternion.identity);
    }

    private void DestroyCoin(Coin coin)
    {
        Destroy(coin.gameObject);
    }
}