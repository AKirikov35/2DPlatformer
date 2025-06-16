using System;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class EnemyDetector : MonoBehaviour
{
    public event Action<Vector3> PlayerDetected;
    public event Action PlayerLost;

    [SerializeField] private LayerMask _layer;
    [SerializeField] private float _detectionRange = 5f;

    private Weapon _weapon;

    public bool IsDetected { get; private set; } = false;
    public bool IsInAttackRange { get; private set; } = false;
    public Vector3 LastDetectedPosition { get; private set; }

    private void Awake()
    {
        _weapon = GetComponent<Weapon>();
    }

    private void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        bool wasDetected = IsDetected;
        IsDetected = false;
        IsInAttackRange = false;

        Collider2D player = Physics2D.OverlapCircle(transform.position, _detectionRange, _layer);
        
        if (player != null && player.TryGetComponent<Player>(out _))
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            IsDetected = true;
            IsInAttackRange = distance <= _weapon.Range;
            LastDetectedPosition = player.transform.position;

            if (!wasDetected)
                PlayerDetected?.Invoke(player.transform.position);
        }
        else if (wasDetected)
        {
            PlayerLost?.Invoke();
        }
    }
}