using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D), typeof(Rotator))]
public class NinjaMover : MonoBehaviour
{
    [field: SerializeField] public float PatrolSpeed { get; private set; } = 3f;
    [field: SerializeField] public float PursuitSpeed { get; private set; } = 4.5f;

    protected Rigidbody2D _rigidbody;
    protected Rotator _rotator;

    public float CurrentDirection { get; protected set; } = 1f;
    public bool IsMoving { get; protected set; }

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rotator = GetComponent<Rotator>();
    }

    public virtual void StopAllMovement()
    {
        _rigidbody.velocity = Vector2.zero;
        IsMoving = false;
    }

    protected void UpdateMovement(Vector2 direction, float speed)
    {
        if (direction.magnitude < 0.1f)
        {
            IsMoving = false;
            return;
        }

        float moveDirection = Mathf.Sign(direction.x);

        if (moveDirection != CurrentDirection)
        {
            _rotator.Rotate(moveDirection);
            CurrentDirection = moveDirection;
        }

        _rigidbody.velocity = new Vector2(direction.x * speed, _rigidbody.velocity.y);
        IsMoving = true;
    }
}