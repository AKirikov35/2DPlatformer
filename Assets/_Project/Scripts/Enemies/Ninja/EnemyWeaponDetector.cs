using UnityEngine;

public class EnemyWeaponDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _hitLayer;

    private EnemyWeapon _weapon;
    private RaycastHit2D _hit;
    private Vector2 _rayOrigin;
    private Vector2 _rayDirection;

    private void Awake()
    {
        _weapon = GetComponent<EnemyWeapon>();
    }

    public void Hit()
    {
        _rayOrigin = transform.position;
        _rayDirection = transform.right;
        _hit = Physics2D.Raycast(_rayOrigin, _rayDirection, _weapon.Range, _hitLayer);

        if (_hit.collider != null && _hit.collider.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_weapon.Damage);
        }
    }
}