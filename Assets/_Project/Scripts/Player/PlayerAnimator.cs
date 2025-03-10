using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Move(float direction)
    {
        _animator.SetFloat(PlayerAnimatorData.MoveSpeed, direction);
    }

    public void Jump()
    {
        _animator.SetBool(PlayerAnimatorData.IsJumping, true);
    }

    public void Land()
    {
        _animator.SetBool(PlayerAnimatorData.IsJumping, false);
    }
}