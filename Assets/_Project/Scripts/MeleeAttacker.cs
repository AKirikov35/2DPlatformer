using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class MeleeAttacker : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayers;

    private Weapon _weapon;

    private void Awake()
    {
        _weapon = GetComponent<Weapon>();
    }

    public void Strike()
    {
        Vector2 origin = transform.position;
        Vector2 direction = transform.right;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, _weapon.Range, _targetLayers);

        if (hit.collider != null && hit.collider.TryGetComponent(out IDamageable target))
            target.TakeDamage(_weapon.Damage);
    }
}