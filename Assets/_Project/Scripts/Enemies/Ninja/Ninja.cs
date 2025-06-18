using System.Collections;
using UnityEngine;

[RequireComponent(typeof(NinjaMover), typeof(Patroller), typeof(Pursuer))]
[RequireComponent(typeof(MeleeAttacker), typeof(Health), typeof(EnemyVision))]
public class Ninja : MonoBehaviour
{
    [SerializeField] private NinjaAnimator _animator;
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private float _attackHitDelayRatio = 0.5f;

    private NinjaMover _mover;
    private Patroller _patroller;
    private Pursuer _pursuer;
    private MeleeAttacker _attacker;
    private Health _health;
    private EnemyVision _vision;

    private Coroutine _attackCoroutine;
    private Vector3 _lastTargetPosition;
    private bool _isAttacking;
    private float _lastAttackTime;
    private WaitForSeconds _waitForHitDelay;
    private WaitForSeconds _waitForAttackFinish;
    private float _lastAttackLength;

    private void Awake()
    {
        _mover = GetComponent<NinjaMover>();
        _patroller = GetComponent<Patroller>();
        _pursuer = GetComponent<Pursuer>();
        _attacker = GetComponent<MeleeAttacker>();
        _health = GetComponent<Health>();
        _vision = GetComponent<EnemyVision>();
    }

    private void Start()
    {
        if (_animator == null)
        {
            enabled = false;
            return;
        }

        UpdateAttackDelays(_animator.GetCurrentAnimationLength());
    }

    private void UpdateAttackDelays(float attackLength)
    {
        if (Mathf.Approximately(attackLength, _lastAttackLength))
            return;

        _lastAttackLength = attackLength;
        float hitDelay = attackLength * _attackHitDelayRatio;
        _waitForHitDelay = new WaitForSeconds(hitDelay);
        _waitForAttackFinish = new WaitForSeconds(attackLength - hitDelay);
    }

    private void OnEnable()
    {
        _health.Died += OnDeath;
        _health.Hurt += OnHurt;
        _vision.OnTargetSpotted += OnTargetSpotted;
    }

    private void OnDisable()
    {
        _health.Died -= OnDeath;
        _health.Hurt -= OnHurt;
        _vision.OnTargetSpotted -= OnTargetSpotted;
    }

    private void FixedUpdate()
    {
        UpdateAnimation();
        HandleAIBehavior();
    }

    private void UpdateAnimation()
    {
        float moveInput = _isAttacking ? 0f : Mathf.Abs(_mover.CurrentDirection);
        _animator.Move(moveInput);
    }

    private void HandleAIBehavior()
    {
        if (_isAttacking) return;

        if (_vision.HasTarget)
        {
            _lastTargetPosition = _vision.LastTargetPosition;

            if (_vision.InAttackRange)
            {
                _pursuer.StopPursuit();

                if (Time.time > _lastAttackTime + _attackCooldown)
                {
                    Attack();
                }
            }
            else
            {
                _patroller.StopPatrol();
                _pursuer.StartPursuit(_lastTargetPosition);
            }
        }
        else
        {
            _pursuer.StopPursuit(); 
            _patroller.StartPatrol();
        }
    }

    private void Attack()
    {
        if (_attackCoroutine != null)
            StopCoroutine(_attackCoroutine);

        _isAttacking = true;
        _mover.Stop();
        _animator.Attack();
        _lastAttackTime = Time.time;

        float currentAttackLength = _animator.GetCurrentAnimationLength();
        UpdateAttackDelays(currentAttackLength);

        _attackCoroutine = StartCoroutine(AttackSequence());
    }

    private void PerformAttack()
    {
        _attacker.Strike();
    }

    private void FinishAttack()
    {
        _isAttacking = false;
    }

    private void OnTargetSpotted(Vector3 position)
    {
        _lastTargetPosition = position;
    }

    private void OnHurt()
    {
        _animator.Hurt();
    }

    private void OnDeath()
    {
        _animator.Died();
        _mover.enabled = false;
        enabled = false;
    }

    private IEnumerator AttackSequence()
    {
        yield return _waitForHitDelay;
        PerformAttack();

        yield return _waitForAttackFinish;
        FinishAttack();
    }
}