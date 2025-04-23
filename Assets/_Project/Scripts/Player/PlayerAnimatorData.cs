using UnityEngine;

public static class PlayerAnimatorData
{
    public static readonly int MoveSpeed = Animator.StringToHash(nameof(MoveSpeed));
    public static readonly int IsJumping = Animator.StringToHash(nameof(IsJumping));
    public static readonly int Hurt = Animator.StringToHash(nameof(Hurt));
    public static readonly int Died = Animator.StringToHash(nameof(Died));
    public static readonly int Attack = Animator.StringToHash(nameof(Attack));
}