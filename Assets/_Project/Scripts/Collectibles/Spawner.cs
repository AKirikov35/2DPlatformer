using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected CollectiblesDetector _detector;
    [SerializeField] protected Collectible _item;
    [SerializeField] protected Transform[] _spawnPosition;

    protected Dictionary<Collectible, int> _collectibles;
    protected int _value;

    public int GetValue(Collectible collectible) => _collectibles[collectible];

    protected virtual void Awake()
    {
        Spawn();
    }

    protected virtual void Spawn()
    {
        for (int i = 0; i < _spawnPosition.Length; i++)
        {
            var instance = Instantiate(_item, _spawnPosition[i].transform.position, Quaternion.identity);
            _collectibles.Add(instance, _value);
        }
    }

    protected virtual void Destroy(Collectible item)
    {
        _collectibles.Remove(item);
        Destroy(item.gameObject);
    }
}