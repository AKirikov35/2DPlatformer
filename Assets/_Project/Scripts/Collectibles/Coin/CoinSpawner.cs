using System;
using System.Collections.Generic;

public class CoinSpawner : Spawner
{
    public event Action<int> CoinsRewarded;

    protected override void Awake()
    {
        _collectibles = new Dictionary<Collectible, int>();
        _value = 1;
        base.Awake();
    }

    private void OnEnable()
    {
        _detector.CoinDetected += Destroy;
    }

    private void OnDisable()
    {
        _detector.CoinDetected -= Destroy;
    }

    protected override void Spawn()
    {
        base.Spawn();
    }

    protected void Destroy(Coin coin)
    {
        CoinsRewarded?.Invoke(_collectibles[coin]);
        base.Destroy(coin);
    }
}