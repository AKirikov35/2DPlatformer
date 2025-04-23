using UnityEngine;

public class FirstAidKitSpawner : MonoBehaviour
{
    [SerializeField] private CollectiblesDetector _detector;
    [SerializeField] private FirstAidKit _firstAidKit;
    [SerializeField] private Transform[] _spawnPosition;

    private void Awake()
    {
        if (_firstAidKit != null && _spawnPosition != null && _spawnPosition.Length > 0)
            Spawn();
    }

    private void OnEnable()
    {
        _detector.FirstAidKitDetected += Destroy;
    }

    private void OnDisable()
    {
        _detector.FirstAidKitDetected -= Destroy;
    }

    private void Spawn()
    {
        for (int i = 0; i < _spawnPosition.Length; i++)
            Instantiate(_firstAidKit, _spawnPosition[i].transform.position, Quaternion.identity);
    }

    private void Destroy(Coin coin)
    {
        Destroy(coin.gameObject);
    }
}