using UnityEngine;
using System.Collections;

public class Patroller : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _waypointThreshold = 0.5f;
    [SerializeField] private float _patrolSpeedMultiplier = 1f;
    [SerializeField] private float _waitingAtWaypoint = 1f;

    private NinjaMover _mover;
    private Coroutine _patrolRoutine;
    private WaitForSeconds _waitingTime;
    private float _thresholdSqr;
    private int _currentIndex;

    private void Awake()
    {
        _mover = GetComponent<NinjaMover>();
        _waitingTime = new WaitForSeconds(_waitingAtWaypoint);
        _thresholdSqr = _waypointThreshold * _waypointThreshold;
    }

    public void StartPatrol()
    {
        if (_waypoints.Length == 0) 
            return;

        StopPatrol();
        _patrolRoutine = StartCoroutine(PatrolRoutine());
    }

    public void StopPatrol()
    {
        if (_patrolRoutine != null) 
            StopCoroutine(_patrolRoutine);

        _mover.Stop();
    }

    private IEnumerator PatrolRoutine()
    {
        while (true)
        {
            Vector2 targetPosition = _waypoints[_currentIndex].position;
            Vector2 direction = targetPosition - (Vector2)transform.position;

            if (direction.sqrMagnitude <= _thresholdSqr)
            {
                _currentIndex = ++_currentIndex % _waypoints.Length;
                _mover.Stop();
                yield return _waitingTime;
                continue;
            }

            _mover.Move(direction.normalized, _patrolSpeedMultiplier);
            yield return null;
        }
    }
}