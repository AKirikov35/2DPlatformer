using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [SerializeField] private SkeletonMover _mover;
    [SerializeField] private SkeletonAnimator _animator;

    [SerializeField] private bool _isPatrol = false;

    private void Update()
    {
        if (_isPatrol == false)
            Stand();
        else
            Patrol();
    }

    private void Patrol()
    {
        _animator.PlayWalk();
        _mover.Patrol();
    }

    private void Stand()
    {
        _animator.PlayIdle();
        _mover.Stand();
    }
}