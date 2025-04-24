using System;
using System.Collections.Generic;

public class AidKitSpawner : Spawner
{
    public event Action<int> HealingReceived;

    protected override void Awake()
    {
        _collectibles = new Dictionary<Collectible, int>();
        _value = 20;
        base.Awake();
    }

    private void OnEnable()
    {
        _detector.AidKitDetected += Destroy;
    }

    private void OnDisable()
    {
        _detector.AidKitDetected -= Destroy;
    }

    protected override void Spawn()
    {
        base.Spawn();
    }

    protected void Destroy(AidKit aidKit)
    {
        HealingReceived?.Invoke(_collectibles[aidKit]);
        base.Destroy(aidKit);
    }
}