using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class FirstAidKit : MonoBehaviour
{
    private CircleCollider2D _collider;

    public int AmountHealthRestored { get; private set; }

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        _collider.isTrigger = true;
        AmountHealthRestored = 20;
    }
}