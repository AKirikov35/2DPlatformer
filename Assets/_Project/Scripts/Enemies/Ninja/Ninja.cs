using UnityEngine;

public class Ninja : MonoBehaviour
{
    [SerializeField] private NinjaAnimator _animator;

    private NinjaMover _mover;

    private void Awake()
    {
        _mover = GetComponent<NinjaMover>();
    }

    private void FixedUpdate()
    {
        float direction = _mover.CurrentDirection;
        _mover.Move(direction);
        _animator.Move(Mathf.Abs(direction));
    }
}