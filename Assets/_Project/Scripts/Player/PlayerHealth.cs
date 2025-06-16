using System;

public class PlayerHealth : Health
{
    public event Action PlayerDied;
    public event Action PlayerHurt;

    protected override void Awake()
    {
        _maxHealth = 100;
        base.Awake();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (CurrentHealth <= 0)
            PlayerDied?.Invoke();
        else
            PlayerHurt?.Invoke();
    }

    public void TakeHeal(int value)
    {
        CurrentHealth += value;

        if (CurrentHealth > _maxHealth)
            CurrentHealth = _maxHealth;
    }
}