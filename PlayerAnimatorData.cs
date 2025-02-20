using UnityEngine;

public static class PlayerAnimatorData
{
    public static readonly int MoveSpeed = Animator.StringToHash(nameof(MoveSpeed));
    public static readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
    public static readonly int IsJumping = Animator.StringToHash(nameof(IsJumping));
}