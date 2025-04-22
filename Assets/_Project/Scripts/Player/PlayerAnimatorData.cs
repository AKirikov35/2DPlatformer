using UnityEngine;

public static class PlayerAnimatorData
{
    public static readonly int MoveSpeed = Animator.StringToHash(nameof(MoveSpeed));
    public static readonly int IsJumping = Animator.StringToHash(nameof(IsJumping));
    public static readonly int IsDamaged = Animator.StringToHash(nameof(IsDamaged));
    public static readonly int IsDied = Animator.StringToHash(nameof(IsDied));
}