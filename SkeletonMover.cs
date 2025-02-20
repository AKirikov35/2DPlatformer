using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SkeletonMover : MonoBehaviour
{
    [SerializeField] private Transform[] _routeWaypoints;
    [SerializeField] private TargetDetector _targetDetector;

    private Rigidbody2D _rigidbody;
    private Coroutine _currentCoroutine;

    private readonly float _moveSpeed = 4f;
    private readonly float _runSpeed = 10f;

    private readonly bool _isMovingToWaypoints = true; 

    private int _currentWaypoint = 0;

    public float Speed { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Patrol()
    {
        RefreshCoroutine();
        _currentCoroutine = StartCoroutine(MoveToWaypoints());
    }

    public void Pursue()
    {
        RefreshCoroutine();
        Move(_targetDetector.TargetPosition.x, _runSpeed);
    }

    private void RefreshCoroutine()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = null;
    }

    private float SetWaypoint()
    {
        Transform target = _routeWaypoints[_currentWaypoint];
        float minDistanceToTarget = 1f;

        if (Vector2.Distance(transform.position, target.position) < minDistanceToTarget)
            _currentWaypoint = ++_currentWaypoint % _routeWaypoints.Length;

        Vector2 direction = (target.position - transform.position).normalized;

        return direction.x;
    }

    private void Move(float direction, float speed)
    {
        Rotate(direction);
        Speed = speed;
        _rigidbody.linearVelocityX = direction * Speed;
    }

    private void Rotate(float direction)
    {
        transform.localScale = new Vector3(direction < 0f ? -1f : 1f, 1f, 1f);
    }

    private IEnumerator MoveToWaypoints()
    {
        while (_isMovingToWaypoints)
        {
            Move(SetWaypoint(), _moveSpeed);
            yield return null;
        }
    }
}