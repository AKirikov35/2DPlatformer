using System;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private AidKitSpawner _aidKit;

    public event Action PlayerDied;
    public event Action PlayerHurt;

    protected override void Awake()
    {
        _maxHealth = 100;
        base.Awake();
    }

    private void OnEnable()
    {
        _aidKit.HealingReceived += TakeHeal;
    }

    private void OnDisable()
    {
        _aidKit.HealingReceived -= TakeHeal;
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