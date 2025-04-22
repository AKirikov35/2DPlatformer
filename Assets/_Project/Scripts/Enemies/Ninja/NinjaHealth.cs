using System;

public class NinjaHealth : Health
{
    public event Action NinjaDied;

    protected override void Awake()
    {
        _maxHealth = 30;
        base.Awake();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (CurrentHealth >= 0)
            NinjaDied?.Invoke();
    }
}