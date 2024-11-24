using UnityEngine;

public class PlayerCoinCollector : MonoBehaviour
{
    private int _coinsCount = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Coin>(out _))
        {
            _coinsCount++;
            Destroy(other.gameObject);
        }
    }
}