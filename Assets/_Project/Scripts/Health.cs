using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public event Action Died;
    public event Action Hurt;

    [SerializeField] private int _max = 100;

    public int Current { get; protected set; }

    private void Awake()
    {
        Current = _max;
    }

    public void TakeDamage(int damage)
    {
        Current -= damage;

        if (Current <= 0)
            Died?.Invoke();
        else
            Hurt?.Invoke();
    }

    public void TakeHeal(int value)
    {
        Current += value;

        if (Current > _max)
            Current = _max;
    }
}