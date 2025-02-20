using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [SerializeField] private TargetDetector _targetDetector;

    private SkeletonMover _mover;

    private void Awake()
    {
        _mover = GetComponent<SkeletonMover>();
    }

    private void Update()
    {
        ChangeState();
    }

    private void ChangeState()
    {
        switch (_targetDetector.SkeletonState)
        {
            case SkeletonState.Patrol:
                Patrol();
                break;

            case SkeletonState.Pursue:
                Pursue();
                break;
        }
    }

    private void Patrol()
    {
        _mover.Patrol();
    }

    private void Pursue()
    {
        _mover.Pursue();
    }
}