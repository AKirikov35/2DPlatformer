using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class Collectible : MonoBehaviour
{
    private CircleCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        _collider.isTrigger = true;
    }
}