using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField] private CoinSpawner _coin;

    public int Gold { get; private set; }

    private void Awake()
    {
        Gold = 0;
    }

    private void OnEnable()
    {
        _coin.CoinsRewarded += GainGold;
    }

    private void OnDisable()
    {
        _coin.CoinsRewarded += GainGold;
    }

    public void GainGold(int value)
    {
        Gold += value;
    }
}