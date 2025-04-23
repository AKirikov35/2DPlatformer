using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Coin : MonoBehaviour
{
    private CircleCollider2D _collider;

    public int Cost { get; private set; } = 1;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        _collider.isTrigger = true;
    }
}