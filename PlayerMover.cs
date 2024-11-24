using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;

    [SerializeField, Range(0f, 10f)] private float _moveSpeed;
    [SerializeField, Range(0f, 10f)] private float _jumpForce;

    private Rigidbody2D _rigidbody;

    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.freezeRotation = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Ground>(out _))
            IsGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Ground>(out _))
            IsGrounded = false;
    }

    public void Move(float direction)
    {
        Rotation(direction);
        _rigidbody.velocity = new Vector2(direction * _moveSpeed, _rigidbody.velocity.y);
    }

    public void Rotation(float direction)
    {
        if (direction > 0)
            _renderer.flipX = false;
        else
            _renderer.flipX = true;
    }

    public void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }
}