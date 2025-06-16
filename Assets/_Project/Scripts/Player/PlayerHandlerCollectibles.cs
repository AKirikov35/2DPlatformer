using UnityEngine;

[RequireComponent(typeof(PlayerHealth), typeof(PlayerWallet))]
public class PlayerHandlerCollectibles : MonoBehaviour
{
    [SerializeField] AidKitSpawner _aidKitSpawner;
    [SerializeField] CoinSpawner _coinSpawner;

    private PlayerHealth _health;
    private PlayerWallet _wallet;

    private void Awake()
    {
        _health = GetComponent<PlayerHealth>();
        _wallet = GetComponent<PlayerWallet>();
    }

    private void OnEnable()
    {
        _aidKitSpawner.HealingReceived += TakeHeal;
        _coinSpawner.CoinsRewarded += GainGold;
    }

    private void OnDisable()
    {
        _aidKitSpawner.HealingReceived -= TakeHeal;
        _coinSpawner.CoinsRewarded -= GainGold;
    }

    private void TakeHeal(int value)
    {
        _health.TakeHeal(value);
    }

    private void GainGold(int value)
    {
        _wallet.GainGold(value);
    }
}