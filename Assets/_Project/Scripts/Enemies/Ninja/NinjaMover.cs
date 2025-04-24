using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class NinjaMover : MonoBehaviour
{
    private const float WaitingTimeAtCheckpoint = 2f;

    [SerializeField, Range(1f, 10f)] private float _moveSpeed = 3f;
    [SerializeField] private Transform[] _routeWaypoints;

    private Rigidbody2D _rigidbody;
    private Rotator _rotator;

    private int _currentWaypoint = 0;
    private Coroutine _patrolCoroutine;

    public float CurrentDirection { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rotator = GetComponent<Rotator>();
    }

    private void Move(float direction)
    {
        Vector2 movement = new Vector2(direction, 0) * _moveSpeed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + movement);
    }

    private float SetWaypoint()
    {
        if (_routeWaypoints.Length == 0)
            return 0f;

        Transform target = _routeWaypoints[_currentWaypoint];
        float minDistanceToTarget = 0.2f;

        if ((transform.position - target.position).sqrMagnitude < minDistanceToTarget)
            return 0f;

        Vector2 direction = (target.position - transform.position).normalized;
        return direction.x;
    }

    public void StartPatrol()
    {
        if (_patrolCoroutine != null)
            StopCoroutine(_patrolCoroutine);

        _patrolCoroutine = StartCoroutine(Patrol());
    }

    public void StartPursue(Vector3 direction)
    {
        if (_patrolCoroutine != null)
            StopCoroutine(_patrolCoroutine);

        Move(direction.x);
    }

    private IEnumerator Patrol()
    {
        while (true)
        {
            CurrentDirection = SetWaypoint();

            if (CurrentDirection == 0f)
            {
                yield return new WaitForSeconds(WaitingTimeAtCheckpoint);
                _currentWaypoint++;

                if (_currentWaypoint >= _routeWaypoints.Length)
                    _currentWaypoint = 0;
            }
            else
            {
                _rotator.Rotate(CurrentDirection);
                Move(CurrentDirection);
            }

            yield return null;
        }
    }
}