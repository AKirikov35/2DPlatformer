using System;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public event Action<Vector3> PlayerDetected;

    [SerializeField] private LayerMask _layer;
    [SerializeField] private float _range;

    private RaycastHit2D _hit;
    private Vector2 _rayOrigin;
    private Vector2 _rayDirection;

    public bool IsDetected { get; private set; } = false;

    private void Update()
    {
        Detected();
    }

    private void Detected()
    {
        _rayOrigin = gameObject.transform.position;
        _rayDirection = gameObject.transform.right;
        _hit = Physics2D.Raycast(_rayOrigin, _rayDirection, _range, _layer);

        if (_hit.collider != null)
            if (_hit.collider.TryGetComponent(out Player player))
            {
                PlayerDetected?.Invoke(player.gameObject.transform.position);
                IsDetected = true;
            }
            else
            {
                IsDetected = false;
            }
    }
}