using UnityEngine;
using System.Collections;

public class Pursuer : NinjaMover
{
    [SerializeField] private float _stopDistance = 0.5f;

    private Coroutine _currentCoroutine;
    private float _stopDistanceSqr;

    protected override void Awake()
    {
        base.Awake();
        _stopDistanceSqr = _stopDistance * _stopDistance;
    }

    public void StartPursue(Vector2 targetPosition)
    {
        StopAllMovement();
        _currentCoroutine = StartCoroutine(Routine(targetPosition));
    }

    public override void StopAllMovement()
    {
        base.StopAllMovement();

        if (_currentCoroutine != null) 
            StopCoroutine(_currentCoroutine);
    }

    private IEnumerator Routine(Vector2 targetPosition)
    {
        while (true)
        {
            Vector2 offset = targetPosition - _rigidbody.position;

            if (offset.sqrMagnitude > _stopDistanceSqr)
            {
                UpdateMovement(offset.normalized, PursuitSpeed);
            }
            else
            {
                IsMoving = false;
                _rigidbody.velocity = Vector2.zero;
            }

            yield return null;
        }
    }
}