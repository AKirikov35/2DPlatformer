using UnityEngine;

public static class NinjaAnimatorData
{
    public static readonly int MoveSpeed = Animator.StringToHash(nameof(MoveSpeed));
    public static readonly int Hurt = Animator.StringToHash(nameof(Hurt));
    public static readonly int Died = Animator.StringToHash(nameof(Died));
}