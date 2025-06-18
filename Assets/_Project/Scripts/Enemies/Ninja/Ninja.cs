using UnityEngine;

[RequireComponent(typeof(NinjaMover), typeof(Health), typeof(EnemyDetector))]
[RequireComponent(typeof(WeaponDetector), typeof(Patroller), typeof(Pursuer))]
public class Ninja : MonoBehaviour
{
    [SerializeField] private float _attackCooldown = 1f;

    private NinjaAnimator _animator;
    private WeaponDetector _weaponDetector;
    private NinjaMover _mover;
    private Health _health;
    private EnemyDetector _detector;
    private Patroller _patroller;
    private Pursuer _pursuer;

    private Vector3 _targetPosition;
    private bool _isAttacking = false;
    private float _lastAttackTime = 0f;

    private void Awake()
    {
        _animator = GetComponentInChildren<NinjaAnimator>();
        _weaponDetector = GetComponent<WeaponDetector>();
        _mover = GetComponent<NinjaMover>();
        _health = GetComponent<Health>();
        _detector = GetComponent<EnemyDetector>();
        _patroller = GetComponent<Patroller>();
        _pursuer = GetComponent<Pursuer>();
    }

    private void FixedUpdate()
    {
        float moveInput = _isAttacking == false ? Mathf.Abs(_mover.CurrentDirection) : 0f;
        _animator.Move(moveInput);

        if (_detector.IsDetected)
        {
            _targetPosition = _detector.LastDetectedPosition;

            if (_detector.IsInAttackRange)
            {
                if (_isAttacking == false && Time.time > _lastAttackTime + _attackCooldown)
                {
                    Attack();
                }
                else
                {
                    _mover.StopAllMovement();
                }
            }
            else if (_isAttacking == false)
            {
                _pursuer.StartPursue(_targetPosition);
            }
        }
        else if (_isAttacking == false)
        {
            _patroller.StartPatrol();
        }
    }

    private void OnEnable()
    {
        _health.Died += Died;
        _health.Hurt += Hurt;
    }

    private void OnDisable()
    {
        _health.Died -= Died;
        _health.Hurt -= Hurt;
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