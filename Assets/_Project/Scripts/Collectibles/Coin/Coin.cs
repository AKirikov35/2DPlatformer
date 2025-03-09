using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Coin : MonoBehaviour
{
    private CircleCollider2D _collider;
    private Animator _animator;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        _animator = GetComponent<Animator>();
        _collider.isTrigger = true;
    }
}