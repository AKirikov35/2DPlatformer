using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class NinjaMover : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolWaypoints;
    [SerializeField] private float _patrolSpeed = 3f;
    [SerializeField] private float _pursuitSpeed = 4.5f;
    [SerializeField] private float _waypointThreshold = 0.5f;
    [SerializeField] private float _pursuitStopDistance = 0.5f;

    private Rigidbody2D _rigidbody;
    private Rotator _rotator;
    private int _currentWaypointIndex = 0;
    private Coroutine _movementCoroutine;
    private bool _isWaiting = false;

    public float CurrentDirection { get; private set; }
    public bool IsMoving { get; private set; }
    public bool IsPursuing { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rotator = GetComponent<Rotator>();
    }

    public void StartPatrol()
    {
        StopAllMovement();
        IsPursuing = false;
        _movementCoroutine = StartCoroutine(PatrolRoutine());
    }

    public void StartPursue(Vector3 targetPosition)
    {
        StopAllMovement();
        IsPursuing = true;
        _movementCoroutine = StartCoroutine(PursueRoutine(targetPosition));
    }

    public void StopAllMovement()
    {
        if (_movementCoroutine != null)
        {
            StopCoroutine(_movementCoroutine);
        }
        _rigidbody.velocity = Vector2.zero;
        CurrentDirection = 0f;
        IsMoving = false;
        _isWaiting = false;
    }

    private IEnumerator PatrolRoutine()
    {
        while (true)
        {
            if (_patrolWaypoints.Length == 0) yield break;

            if (_isWaiting)
            {
                _currentWaypointIndex = ++_currentWaypointIndex % _patrolWaypoints.Length;
                _isWaiting = false;
                continue;
            }

            Vector2 targetPosition = _patrolWaypoints[_currentWaypointIndex].position;
            Vector2 direction = (targetPosition - _rigidbody.position).normalized;

            UpdateMovement(direction, _patrolSpeed);
            IsMoving = true;

            if (Vector2.Distance(transform.position, targetPosition) <= _waypointThreshold)
            {
                IsMoving = false;
                _rigidbody.velocity = Vector2.zero;
                _isWaiting = true;
            }

            yield return null;
        }
    }

    private IEnumerator PursueRoutine(Vector3 targetPosition)
    {
        while (true)
        {
            float distance = Vector2.Distance(transform.position, targetPosition);

            if (distance > _pursuitStopDistance)
            {
                Vector2 direction = ((Vector2)targetPosition - _rigidbody.position).normalized;
                UpdateMovement(direction, _pursuitSpeed);
                IsMoving = true;
            }
            else
            {
                IsMoving = false;
                _rigidbody.velocity = Vector2.zero;
            }

            yield return null;
        }
    }

    private void UpdateMovement(Vector2 direction, float speed)
    {
        float moveDirection = Mathf.Sign(direction.x);

        if (Mathf.Abs(moveDirection - CurrentDirection) > 0.1f)
        {
            _rotator.Rotate(moveDirection);
            CurrentDirection = moveDirection;
        }

        _rigidbody.velocity = new Vector2(direction.x * speed, _rigidbody.velocity.y);
    }
}
