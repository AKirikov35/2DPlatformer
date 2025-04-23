using UnityEngine;

public class NinjaAnimator : MonoBehaviour
{
    private Animator _animator;

    public float DiedTime { set; private get; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Move(float direction) => _animator.SetFloat(NinjaAnimatorData.MoveSpeed, direction);
    public void Hurt() => _animator.SetTrigger(NinjaAnimatorData.Hurt);
    public void Died() => _animator.SetTrigger(NinjaAnimatorData.Died);
}