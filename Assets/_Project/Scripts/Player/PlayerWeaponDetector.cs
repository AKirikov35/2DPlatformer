using UnityEngine;

public class PlayerWeaponDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _hitLayer;

    private PlayerWeapon _weapon;
    private RaycastHit2D _hit;
    private Vector2 _rayOrigin;
    private Vector2 _rayDirection;

    private void Awake()
    {
        _weapon = GetComponent<PlayerWeapon>();
    }

    public void Hit()
    {
        _rayOrigin = gameObject.transform.position;
        _rayDirection = gameObject.transform.right;
        _hit = Physics2D.Raycast(_rayOrigin, _rayDirection, _weapon.Range, _hitLayer);

        if (_hit.collider != null)
            if (_hit.collider.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(_weapon.Damage);
    }
}