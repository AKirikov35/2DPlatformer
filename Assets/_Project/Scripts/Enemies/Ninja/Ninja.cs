using UnityEngine;

public class Ninja : MonoBehaviour
{
    [SerializeField] private NinjaAnimator _animator;

    private NinjaMover _mover;
    private NinjaHealth _health;

    private void Awake()
    {
        _mover = GetComponent<NinjaMover>();
        _health = GetComponent<NinjaHealth>();
    }

    private void FixedUpdate()
    {
        float direction = _mover.CurrentDirection;
        _mover.Move(direction);
        _animator.Move(Mathf.Abs(direction));
    }

    private void OnEnable()
    {
        _health.NinjaDied += Died;
        _health.NinjaHurt += Hurt;
    }

    private void OnDisable()
    {
        _health.NinjaDied -= Died;
        _health.NinjaHurt -= Hurt;
    }

    private void Died()
    {
        _animator.Died();
        _mover.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void Hurt()
    {
        _animator.Hurt();
    }
}