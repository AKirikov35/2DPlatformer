using UnityEngine;

public class Ninja : MonoBehaviour
{
    [SerializeField] private NinjaAnimator _animator;
    [SerializeField] private EnemyWeaponDetector _weaponDetector;
    [SerializeField] private float _attackCooldown = 1f;

    private NinjaMover _mover;
    private NinjaHealth _health;
    private EnemyDetector _detector;

    private Vector3 _targetPosition;
    private bool _isAttacking = false;
    private float _lastAttackTime = 0f;

    private void Awake()
    {
        _mover = GetComponent<NinjaMover>();
        _health = GetComponent<NinjaHealth>();
        _detector = GetComponent<EnemyDetector>();
    }

    private void FixedUpdate()
    {
        float moveInput = _mover.IsMoving && !_isAttacking ? Mathf.Abs(_mover.CurrentDirection) : 0f;
        _animator.Move(moveInput);

        if (_detector.IsDetected)
        {
            _targetPosition = _detector.LastDetectedPosition;

            if (_detector.IsInAttackRange)
            {
                if (!_isAttacking && Time.time > _lastAttackTime + _attackCooldown)
                {
                    Attack();
                }
                else
                {
                    _mover.StopAllMovement();
                }
            }
            else if (!_isAttacking)
            {
                _mover.StartPursue(_targetPosition);
            }
        }
        else if (!_isAttacking)
        {
            _mover.StartPatrol();
        }
    }

    private void OnEnable()
    {
        _health.NinjaDied += Died;
        _health.NinjaHurt += Hurt;
    }

    private void OnDisable()
    {
        _health.NinjaDied -= Died;
        _health.NinjaHurt -= Hurt;
    }

    private void Attack()
    {
        _isAttacking = true;
        _mover.StopAllMovement();
        _animator.Attack();
        _lastAttackTime = Time.time;

        Invoke(nameof(PerformAttack), _animator.GetCurrentAnimationLength() * 0.5f);
        Invoke(nameof(FinishAttack), _animator.GetCurrentAnimationLength());
    }

    private void PerformAttack()
    {
        _weaponDetector.Hit();
    }

    private void FinishAttack()
    {
        _isAttacking = false;
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