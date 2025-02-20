using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject[] _spawnPoints;

    private void Awake()
    {
        if (_prefab != null && _spawnPoints != null)
            Spawn();
    }

    private void Spawn()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
            Instantiate(_prefab, _spawnPoints[i].transform.position, Quaternion.identity);
    }
}
