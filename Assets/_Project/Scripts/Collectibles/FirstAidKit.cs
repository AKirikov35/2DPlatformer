using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class FirstAidKit : MonoBehaviour
{
    private CircleCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        _collider.isTrigger = true;
    }
}