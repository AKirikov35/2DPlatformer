using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField, Range(4f, 10f)] private float _moveSpeed = 6f;
    [SerializeField, Range(5f, 12f)] private float _jumpForce = 8f;

    public float MoveSpeed { get; private set; } = 0f;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(float direction)
    {
        Rotate(direction);
        MoveSpeed = direction * _moveSpeed;
        _rigidbody.linearVelocityX = direction * _moveSpeed;
    }

    public void Jump()
    {
        _rigidbody.linearVelocityY = Vector2.up.y * _jumpForce;
    }

    private void Rotate(float direction)
    {
        transform.localScale = new Vector3(direction < 0f ? -1f : 1f, 1f, 1f);
    }
}