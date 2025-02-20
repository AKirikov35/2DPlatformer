using UnityEngine;

public class Player : MonoBehaviour
{
    private GroundDetector _groundDetector;
    private InputReader _inputReader;
    private PlayerMover _mover;

    private void Awake()
    {
        _groundDetector = GetComponent<GroundDetector>();
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<PlayerMover>();
    }

    private void FixedUpdate()
    {
        if (_inputReader.MoveDirection != 0)
            _mover.Move(_inputReader.MoveDirection);

        if (_inputReader.GetIsJump() && _groundDetector.IsGround)
            _mover.Jump();
    }
}