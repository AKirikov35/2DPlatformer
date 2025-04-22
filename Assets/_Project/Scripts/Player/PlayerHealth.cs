using System;

public class PlayerHealth : Health
{
    public event Action PlayerDied;

    protected override void Awake()
    {
        _maxHealth = 100;
        base.Awake();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (CurrentHealth >= 0)
            PlayerDied?.Invoke();
    }

    public void TakeHeal(int heal)
    {
        CurrentHealth += heal;

        if (CurrentHealth > _maxHealth)
            CurrentHealth = _maxHealth;
    }
}