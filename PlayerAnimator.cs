using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GroundDetector _groundDetector;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetFloat(PlayerAnimatorData.MoveSpeed, Mathf.Abs(_mover.MoveSpeed));
        _animator.SetBool(PlayerAnimatorData.IsGrounded, _groundDetector.IsGround);
        _animator.SetBool(PlayerAnimatorData.IsJumping, !_groundDetector.IsGround);
    }
}