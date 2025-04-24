using UnityEngine;

public class Ninja : MonoBehaviour
{
    [SerializeField] private NinjaAnimator _animator;

    private NinjaMover _mover;
    private NinjaHealth _health;
    private EnemyDetector _detector;

    private Vector3 _targetPosition;

    private void Awake()
    {
        _mover = GetComponent<NinjaMover>();
        _health = GetComponent<NinjaHealth>();
        _detector = GetComponent<EnemyDetector>();
    }

    private void FixedUpdate()
    {
        float direction = _mover.CurrentDirection;
        _animator.Move(Mathf.Abs(direction));

        if (_detector.IsDetected)
            Pursue(_targetPosition);
        else
            Patrol();
    }

    private void OnEnable()
    {
        _health.NinjaDied += Died;
        _health.NinjaHurt += Hurt;
        _detector.PlayerDetected += Pursue;
    }

    private void OnDisable()
    {
        _health.NinjaDied -= Died;
        _health.NinjaHurt -= Hurt;
        _detector.PlayerDetected -= Pursue;
    }

    private void Patrol()
    {
        _mover.StartPatrol();
    }

    private void Pursue(Vector3 target)
    {
        _targetPosition = target;
        _mover.StartPursue(target);
    }

    private void Died()
    {
        _animator.Died();
        _mover.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void Hurt()
    {
        _animator.Hurt();
    }
}