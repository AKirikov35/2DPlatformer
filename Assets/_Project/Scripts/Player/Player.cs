using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerAnimator _animator;

    private GroundDetector _groundDetector;
    private InputReader _inputReader;
    private PlayerMover _mover;
    private PlayerHealth _health;

    private void Awake()
    {
        _groundDetector = GetComponent<GroundDetector>();
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<PlayerMover>();
        _health = GetComponent<PlayerHealth>();
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
    }

    private void OnEnable()
    {
        _health.PlayerDied += Died;
    }

    private void OnDisable()
    {
        _health.PlayerDied -= Died;
    }

    private void Died()
    {
        _inputReader.gameObject.SetActive(false);
        _mover.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}