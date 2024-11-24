using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private PlayerCoinCollector _coinCollector;

    private void Update()
    {
        if (InputReader.IsMoveInput())
        {
            _animator.PlayRun();
            _mover.Move(Input.GetAxis(InputReader.Horizontal));
        }
        else
        {
            _animator.PlayIdle();
        }

        if (Input.GetKeyDown(InputReader.KeyJump) && _mover.IsGrounded)
            _mover.Jump();
    }
}