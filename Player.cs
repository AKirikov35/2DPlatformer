using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    const string Horizontal = nameof(Horizontal);
    const KeyCode KeyJump = KeyCode.Space;

    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Animator _animator;

    [SerializeField, Range(0f, 10f)] private float _moveSpeed;
    [SerializeField, Range(0f, 10f)] private float _jumpForce;

    [SerializeField] private AnimationClip _idle;
    [SerializeField] private AnimationClip _run;

    private Rigidbody2D _rigidbody;

    private int _coinsCount = 0;

    private bool _isGrounded;

    private void Awake()
    {
        InitComponent();
    }

    private void Update()
    {
        if (Input.GetAxis(Horizontal) != 0)
        {
            _animator.Play(_run.name);
            Move(Input.GetAxis(Horizontal));
        }
        else
        {
            _animator.Play(_idle.name);
        }

        if (Input.GetKeyDown(KeyJump) && _isGrounded)
            Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Ground>(out _))
            _isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Ground>(out _))
            _isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<Coin>(out _))
        {
            _coinsCount++;
            Debug.Log("Coins: " + _coinsCount);
            Destroy(other.gameObject);
        }           
    }

    private void InitComponent()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.freezeRotation = true;
    }

    private void Move(float direction)
    {
        Rotate(direction);
        _rigidbody.velocity = new Vector2(direction * _moveSpeed, _rigidbody.velocity.y);
    }

    private void Rotate(float direction)
    {
        if (direction > 0)
            _renderer.flipX = false;
        else
            _renderer.flipX = true;
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }
}