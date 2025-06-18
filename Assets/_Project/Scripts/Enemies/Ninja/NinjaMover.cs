using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D), typeof(Rotator))]
public class NinjaMover : MonoBehaviour
{
    [SerializeField] private float _defaultSpeed = 3f;
    [SerializeField] private float _minThreshold = 0.1f;

    private Rigidbody2D _rigidbody;
    private Rotator _rotator;
    private float _minThresholdSqr;

    public float CurrentDirection { get; private set; } = 1f;
    public bool IsMoving { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rotator = GetComponent<Rotator>();
        _minThresholdSqr = _minThreshold * _minThreshold;
    }

    public void Move(Vector2 direction, float speedMultiplier = 1f)
    {
        if (direction.sqrMagnitude < _minThresholdSqr)
        {
            Stop();
            return;
        }

        float moveDirection = Mathf.Sign(direction.x);

        if (moveDirection != CurrentDirection)
        {
            _rotator.Rotate(moveDirection);
            CurrentDirection = moveDirection;
        }

        _rigidbody.velocity = direction.normalized * (_defaultSpeed * speedMultiplier);
        IsMoving = true;
    }

    public void Stop()
    {
        _rigidbody.velocity = Vector2.zero;
        IsMoving = false;
    }
}