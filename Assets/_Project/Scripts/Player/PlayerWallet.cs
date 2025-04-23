using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    private CollectiblesDetector _detector;

    public int Gold { get; private set; }

    private void Awake()
    {
        _detector = GetComponent<CollectiblesDetector>();
        Gold = 0;
    }

    private void OnEnable()
    {
        _detector.CoinDetected += GainGold;
    }

    private void OnDisable()
    {
        _detector.CoinDetected += GainGold;
    }

    public void GainGold(Coin coin)
    {
        Gold += coin.Cost;
    }
}