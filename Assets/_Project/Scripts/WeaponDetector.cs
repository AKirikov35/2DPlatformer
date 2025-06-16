using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class WeaponDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _hitLayer;
    private Weapon _weapon;

    private void Awake()
    {
        _weapon = GetComponent<Weapon>();
    }

    public void Hit()
    {
        Vector2 rayOrigin = transform.position;
        Vector2 rayDirection = transform.right;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, _weapon.Range, _hitLayer);

        if (hit.collider != null && hit.collider.TryGetComponent(out IDamageable damageable))
            damageable.TakeDamage(_weapon.Damage);
    }
}