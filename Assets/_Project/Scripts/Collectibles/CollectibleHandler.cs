public class CollectibleHandler : ICoinVisitor, IAidKitVisitor
{
    private readonly Health _health;
    private readonly PlayerWallet _wallet;
    private readonly CoinSpawner _coinSpawner;
    private readonly AidKitSpawner _aidKitSpawner;

    public CollectibleHandler(Health health, PlayerWallet wallet, CoinSpawner coinSpawner, AidKitSpawner aidKitSpawner)
    {
        _health = health;
        _wallet = wallet;
        _coinSpawner = coinSpawner;
        _aidKitSpawner = aidKitSpawner;
    }

    public void Visit(Coin coin)
    {
        int value = _coinSpawner.GetValue(coin);
        _wallet.GainGold(value);
        _coinSpawner.Destroy(coin);
    }

    public void Visit(AidKit aidKit)
    {
        int value = _aidKitSpawner.GetValue(aidKit);
        _health.TakeHeal(value);
        _aidKitSpawner.Destroy(aidKit);
    }
}