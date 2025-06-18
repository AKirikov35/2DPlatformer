using UnityEngine;
using System.Collections;

public class Patroller : NinjaMover
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _waypointThreshold = 0.5f;

    private Coroutine _currentCoroutine;
    private float _waypointThresholdSqr;
    private int _currentWaypointIndex = 0;
    private bool _isWaiting = false;

    protected override void Awake()
    {
        base.Awake();
        _waypointThresholdSqr = _waypointThreshold * _waypointThreshold;
    }

    public void StartPatrol()
    {
        if (_waypoints.Length == 0)
            return;

        StopAllMovement();
        _currentCoroutine = StartCoroutine(Routine());
    }

    public override void StopAllMovement()
    {
        base.StopAllMovement();

        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _isWaiting = false;
    }

    private IEnumerator Routine()
    {
        while (true)
        {
            if (_waypoints.Length == 0) 
                yield break;

            if (_isWaiting)
            {
                _currentWaypointIndex = ++_currentWaypointIndex % _waypoints.Length;
                _isWaiting = false;
                continue;
            }

            Vector2 targetPosition = _waypoints[_currentWaypointIndex].position;
            Vector2 offset = targetPosition - _rigidbody.position;

            if (offset.sqrMagnitude < 0.001f)
            {
                IsMoving = false;
                _rigidbody.velocity = Vector2.zero;
                _isWaiting = true;
                continue;
            }

            UpdateMovement(offset.normalized, PatrolSpeed);

            if (offset.sqrMagnitude <= _waypointThresholdSqr)
            {
                IsMoving = false;
                _rigidbody.velocity = Vector2.zero;
                _isWaiting = true;
            }

            yield return null;
        }
    }
}