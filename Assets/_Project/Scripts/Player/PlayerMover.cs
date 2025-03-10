using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class PlayerMover : MonoBehaviour
{
    private const float SpeedThreshold = 0.5f;

    [SerializeField, Range(1f, 10f)] private float _moveSpeed = 4f;
    [SerializeField, Range(3f, 12f)] private float _jumpForce = 7f;

    public float MoveSpeed { get; private set; } = 0f;

    private Rigidbody2D _rigidbody;
    private Rotator _rotator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rotator = GetComponent<Rotator>();
    }

    public void Move(float direction)
    {
        Rotate(direction);

        float moveSpeed = direction * _moveSpeed;

        if (Mathf.Abs(moveSpeed) < SpeedThreshold)
            moveSpeed = 0f;

        Vector3 currentVelocity = _rigidbody.velocity;
        _rigidbody.velocity = new Vector2(moveSpeed, currentVelocity.y);
    }

    public void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }

    public void Rotate(float direction)
    {
        _rotator.Rotate(direction);
    }
}