using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected Collectible _itemPrefab;
    [SerializeField] protected Transform[] _spawnPositions;

    protected Dictionary<Collectible, int> _collectibles = new Dictionary<Collectible, int>();
    protected int _defaultValue;

    public int GetValue(Collectible collectible) => _collectibles[collectible];

    protected virtual void Awake()
    {
        Spawn();
    }

    protected virtual void Spawn()
    {
        foreach (var position in _spawnPositions)
        {
            var instance = Instantiate(_itemPrefab, position.position, Quaternion.identity);
            _collectibles.Add(instance, _defaultValue);
        }
    }

    public virtual void Destroy(Collectible item)
    {
        _collectibles.Remove(item);
        Destroy(item.gameObject);
    }
}