using System;
using UnityEngine;

[RequireComponent(typeof(GroundDetector), typeof(InputReader))]
[RequireComponent(typeof(PlayerMover), typeof(Health), typeof(MeleeAttacker))]
public class Player : MonoBehaviour
{
    private PlayerAnimator _animator;
    private GroundDetector _groundDetector;
    private InputReader _inputReader;
    private PlayerMover _mover;
    private Health _health;
    private MeleeAttacker _meleeAttacker;

    private void Awake()
    {
        _animator = GetComponentInChildren<PlayerAnimator>();
        _groundDetector = GetComponent<GroundDetector>();
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<PlayerMover>();
        _health = GetComponent<Health>();
        _meleeAttacker = GetComponent<MeleeAttacker>();
    }

    private void FixedUpdate()
    {
        if (_inputReader.MoveDirection != 0)
        {
            float direction = _inputReader.MoveDirection;
            _animator.Move(Math.Abs(direction));
            _mover.Move(direction);
        }

        if (_groundDetector.IsGrounded())
        {
            _animator.Land();
        }

        if (_inputReader.GetIsJump() && _groundDetector.IsGrounded())
        {
            _animator.Jump();
            _mover.Jump();
        }

        if (_inputReader.GetIsAttack())
        {
            _animator.Attack();
            _meleeAttacker.Strike();
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
        _health.Hurt += Hurt;
    }

    private void Hurt()
    {
        _animator.Hurt();
    }

    private void Died()
    {
        _inputReader.gameObject.SetActive(false);
        _mover.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}