using UnityEngine;

public abstract class Health : MonoBehaviour, IDamageable
{
    protected int _maxHealth;

    public int CurrentHealth { get; protected set; }

    protected virtual void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
    }
}