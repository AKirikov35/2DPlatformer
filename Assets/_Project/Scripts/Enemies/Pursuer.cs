using UnityEngine;
using System.Collections;

public class Pursuer : MonoBehaviour
{
    [SerializeField] private float _stopDistance = 0.5f;
    [SerializeField] private float _pursuitSpeedMultiplier = 1.5f;

    private NinjaMover _mover;
    private Coroutine _pursuitRoutine;
    private float _stopDistanceSqr;

    private void Awake()
    {
        _mover = GetComponent<NinjaMover>();
        _stopDistanceSqr = _stopDistance * _stopDistance;
    }

    public void StartPursuit(Vector2 targetPosition)
    {
        StopPursuit();
        _pursuitRoutine = StartCoroutine(PursuitRoutine(targetPosition));
    }

    public void StopPursuit()
    {
        if (_pursuitRoutine != null) 
            StopCoroutine(_pursuitRoutine);

        _mover.Stop();
    }

    private IEnumerator PursuitRoutine(Vector2 targetPosition)
    {
        while (true)
        {
            Vector2 direction = targetPosition - (Vector2)transform.position;
            float sqrDistance = direction.sqrMagnitude;

            if (sqrDistance > _stopDistanceSqr)
            {
                _mover.Move(direction.normalized, _pursuitSpeedMultiplier);
            }
            else
            {
                _mover.Stop();
                yield break;
            }

            yield return null;
        }
    }
}