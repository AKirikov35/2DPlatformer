using System;
using UnityEngine;

public class CollectiblesDetector : MonoBehaviour
{
    public event Action<Collectible> CollectibleDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Collectible collectible))
            CollectibleDetected?.Invoke(collectible);
    }
}