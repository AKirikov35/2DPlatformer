using UnityEngine;

public class NinjaAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Move(float direction)
    {
        _animator.SetFloat(NinjaAnimatorData.MoveSpeed, direction);
    }

    public void IsDamaged()
    {
        _animator.SetBool(NinjaAnimatorData.IsDamaged, true);
    }

    public void IsDied()
    {
        _animator.SetBool(NinjaAnimatorData.IsDied, true);
    }
}