using System;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class EnemyVision : MonoBehaviour
{
    [SerializeField] private LayerMask _layers;
    [SerializeField] private float _range = 5f;
    private Weapon _weapon;

    public event Action<Vector3> OnTargetSpotted;
    public event Action OnTargetLost;

    public bool HasTarget { get; private set; } = false;
    public bool InAttackRange { get; private set; } = false;
    public Vector3 LastTargetPosition { get; private set; }

    private void Awake()
    {
        _weapon = GetComponent<Weapon>();
    }

    private void Update()
    {
        ScanForTarget();
    }

    private void ScanForTarget()
    {
        bool previouslyHadTarget = HasTarget;
        HasTarget = false;
        InAttackRange = false;

        Collider2D detectedTarget = Physics2D.OverlapCircle(transform.position, _range, _layers);

        if (detectedTarget != null && detectedTarget.TryGetComponent<Player>(out _))
        {
            float sqrDistance = (detectedTarget.transform.position - transform.position).sqrMagnitude;
            float sqrWeaponRange = _weapon.Range * _weapon.Range;
            HasTarget = true;
            InAttackRange = sqrDistance <= sqrWeaponRange;
            LastTargetPosition = detectedTarget.transform.position;

            if (previouslyHadTarget == false)
                OnTargetSpotted?.Invoke(detectedTarget.transform.position);
        }
        else if (previouslyHadTarget)
        {
            OnTargetLost?.Invoke();
        }
    }
}