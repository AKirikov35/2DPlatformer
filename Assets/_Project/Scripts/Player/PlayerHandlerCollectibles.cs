using UnityEngine;

[RequireComponent(typeof(Health), typeof(PlayerWallet))]
public class PlayerHandlerCollectibles : MonoBehaviour
{
    [SerializeField] private CollectiblesDetector _detector;
    [SerializeField] private CoinSpawner _coinSpawner;
    [SerializeField] private AidKitSpawner _aidKitSpawner;

    private Health _health;
    private PlayerWallet _wallet;
    private CollectibleHandler _handler;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _wallet = GetComponent<PlayerWallet>();
        _handler = new CollectibleHandler(_health, _wallet, _coinSpawner, _aidKitSpawner);
    }

    private void OnEnable()
    {
        _detector.CollectibleDetected += OnCollectibleDetected;
    }

    private void OnDisable()
    {
        _detector.CollectibleDetected -= OnCollectibleDetected;
    }

    private void OnCollectibleDetected(Collectible collectible)
    {
        collectible.Accept(_handler);
    }
}