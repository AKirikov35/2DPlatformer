using UnityEngine;

[RequireComponent(typeof(Animator))]
public class NinjaAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public float GetCurrentAnimationLength()
    {
        if (_animator == null || !_animator.isInitialized)
            return 0f;

        var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.length == 0)
            return 0.5f;

        return stateInfo.length;
    }

    public void Move(float direction)
    {
        _animator.SetFloat(NinjaAnimatorData.MoveSpeed, direction);
    }

    public void Hurt()
    {
        _animator.SetTrigger(NinjaAnimatorData.Hurt);
    }

    public void Died()
    {
        _animator.SetTrigger(NinjaAnimatorData.Died);
    }

    public void Attack()
    {
        _animator.SetTrigger(NinjaAnimatorData.Attack);
    }
}