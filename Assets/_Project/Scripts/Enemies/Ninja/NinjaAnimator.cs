using UnityEngine;

public class NinjaAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    { 
        _animator = GetComponent<Animator>();
    }

    public float GetCurrentAnimationLength() =>_animator.GetCurrentAnimatorStateInfo(0).length;

    public void Move(float direction) => _animator.SetFloat(NinjaAnimatorData.MoveSpeed, direction);
    public void Hurt() => _animator.SetTrigger(NinjaAnimatorData.Hurt);
    public void Died() => _animator.SetTrigger(NinjaAnimatorData.Died);
    public void Attack() => _animator.SetTrigger(NinjaAnimatorData.Attack); 
}