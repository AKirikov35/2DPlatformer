using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SkeletonAnimator : MonoBehaviour
{
    [SerializeField] private AnimationClip _idle;
    [SerializeField] private AnimationClip _walk;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayIdle()
    {
        _animator.Play(_idle.name);
    }

    public void PlayWalk()
    {
        _animator.Play(_walk.name);
    }
}