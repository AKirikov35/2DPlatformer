using UnityEngine;

[RequireComponent(typeof(Animator))]
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

    public void Hurt()
    {
        _animator.SetBool(PlayerAnimatorData.Hurt, true);
    }

    public void Died()
    {
        _animator.SetBool(PlayerAnimatorData.Died, true);
    }

    public void Attack()
    {
        _animator.SetTrigger(PlayerAnimatorData.Attack);
    }
}