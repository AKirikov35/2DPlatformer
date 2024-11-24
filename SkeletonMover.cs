using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SkeletonMover : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Transform[] _routeWaypoints;

    [SerializeField, Range(0f, 10f)] private float _moveSpeed;

    private Rigidbody2D _rigidbody;
    private Coroutine _currentCoroutine;

    private int _currentWaypoint = 0;

    private bool _isMovingToWaypoints = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.freezeRotation = true;
    }

    public void Patrol()
    {
        RefreshCoroutine();
        _currentCoroutine = StartCoroutine(MoveToWaypoints());
    }

    public void Stand()
    {
        RefreshCoroutine();
    }

    private void RefreshCoroutine()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = null;
    }

    private void Move(Vector2 direction)
    {
        Rotation(direction.x);
        _rigidbody.MovePosition((Vector2)transform.position + direction * _moveSpeed * Time.deltaTime);
    }

    private void Rotation(float direction)
    {
        if (direction > 0)
            _renderer.flipX = false;
        else
            _renderer.flipX = true;
    }

    private Vector2 SetWaypoint()
    {
        Transform target = _routeWaypoints[_currentWaypoint];
        float minDistanceToTarget = 0.2f;

        if (Vector2.Distance(transform.position, target.position) < minDistanceToTarget)
            _currentWaypoint = ++_currentWaypoint % _routeWaypoints.Length;

        Vector2 direction = (target.position - transform.position).normalized;

        return direction;
    }

    private IEnumerator MoveToWaypoints()
    {
        while (_isMovingToWaypoints)
        {
            Move(SetWaypoint());
            yield return null;
        }
    }
}