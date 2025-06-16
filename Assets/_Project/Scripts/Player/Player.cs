using System;
using UnityEngine;

[RequireComponent(typeof(GroundDetector), typeof(InputReader))]
[RequireComponent(typeof(PlayerMover), typeof(PlayerHealth), typeof(WeaponDetector))]
public class Player : MonoBehaviour
{
    private PlayerAnimator _animator;
    private GroundDetector _groundDetector;
    private InputReader _inputReader;
    private PlayerMover _mover;
    private PlayerHealth _health;
    private WeaponDetector _weaponDetector;

    private void Awake()
    {
        _animator = GetComponentInChildren<PlayerAnimator>();
        _groundDetector = GetComponent<GroundDetector>();
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<PlayerMover>();
        _health = GetComponent<PlayerHealth>();
        _weaponDetector = GetComponent<WeaponDetector>();
    }

    private void FixedUpdate()
    {
        if (_inputReader.MoveDirection != 0)
        {
            float direction = _inputReader.MoveDirection;
            _animator.Move(Math.Abs(direction));
            _mover.Move(direction);
        }

        if (_groundDetector.IsGround)
        {
            _animator.Land();
        }

        if (_inputReader.GetIsJump() && _groundDetector.IsGround)
        {
            _animator.Jump();
            _mover.Jump();
        }

        if (_inputReader.GetIsAttack())
        {
            _animator.Attack();
            _weaponDetector.Hit();
        }
    }

    private void OnEnable()
    {
        _health.PlayerDied += Died;
        _health.PlayerHurt += Hurt;
    }

    private void OnDisable()
    {
        _health.PlayerDied -= Died;
        _health.PlayerHurt += Hurt;
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