using System;

public class NinjaHealth : Health
{
    public event Action NinjaDied;
    public event Action NinjaHurt;

    protected override void Awake()
    {
        _maxHealth = 30;
        base.Awake();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (CurrentHealth <= 0)
            NinjaDied?.Invoke();
        else
            NinjaHurt?.Invoke();
    }
}