using System;

public class PlayerHealth : Health
{
    public event Action PlayerDied;
    public event Action PlayerHurt;

    private CollectiblesDetector _detector;

    protected override void Awake()
    {
        _detector = GetComponent<CollectiblesDetector>();
        _maxHealth = 100;
        base.Awake();
    }

    private void OnEnable()
    {
        _detector.FirstAidKitDetected += TakeHeal;
    }

    private void OnDisable()
    {
        _detector.FirstAidKitDetected -= TakeHeal;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (CurrentHealth <= 0)
            PlayerDied?.Invoke();
        else
            PlayerHurt?.Invoke();
    }

    public void TakeHeal(FirstAidKit firstAidKit)
    {
        CurrentHealth += firstAidKit.AmountHealthRestored;

        if (CurrentHealth > _maxHealth)
            CurrentHealth = _maxHealth;
    }
}