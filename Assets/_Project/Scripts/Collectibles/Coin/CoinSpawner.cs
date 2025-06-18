public class CoinSpawner : Spawner
{
    protected override void Awake()
    {
        _defaultValue = 1;
        base.Awake();
    }
}