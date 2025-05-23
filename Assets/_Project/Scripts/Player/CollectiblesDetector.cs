using System;
using UnityEngine;

public class CollectiblesDetector : MonoBehaviour
{
    public event Action<Coin> CoinDetected;
    public event Action<AidKit> AidKitDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
            CoinDetected?.Invoke(coin);

        if (collision.TryGetComponent(out AidKit aidKit))
            AidKitDetected?.Invoke(aidKit);
    }
}