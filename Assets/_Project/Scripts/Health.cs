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
        if (damage <= 0) 
            return;

        Current = Math.Clamp(Current - damage, 0, _max);

        if (Current <= 0)
            Died?.Invoke();
        else
            Hurt?.Invoke();
    }

    public void TakeHeal(int value)
    {
        if (value <= 0) 
            return;

        Current = Math.Clamp(Current + value, 0, _max);
    }
}