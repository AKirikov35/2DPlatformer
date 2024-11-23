using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Skeleton : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Animator _animator;

    [SerializeField] private Transform[] _routeWaypoints;
    [SerializeField, Range(0f, 10f)] private float _moveSpeed;

    [SerializeField] private AnimationClip _idle;
    [SerializeField] private AnimationClip _walk;

    private Rigidbody2D _rigidbody;
    private int _currentWaypoint = 0;

    private bool _isMovingToWaypoints = true;

    private void Awake()
    {
        InitComponent();
    }

    private void Start()
    {
        StartCoroutine(MoveToWaypoints());
    }

    private void InitComponent()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.freezeRotation = true;
    }

    private Vector2 SetWaypoint()
    {
        Transform target = _routeWaypoints[_currentWaypoint];
        float minDistanceToTarget = 0.2f;

        if (Vector2.Distance(transform.position, target.position) < minDistanceToTarget)
            _currentWaypoint = ++_currentWaypoint % _routeWaypoints.Length;

        Vector2 direction = (target.position - transform.position).normalized;

        return direction;
    }

    private void Move(Vector2 direction)
    {        
        _animator.Play(_walk.name);

        Rotate(direction.x);
        _rigidbody.MovePosition((Vector2)transform.position + direction * _moveSpeed * Time.deltaTime);
    }

    private void Rotate(float direction)
    {
        if (direction > 0)
            _renderer.flipX = false;
        else
            _renderer.flipX = true;
    }

    private IEnumerator MoveToWaypoints()
    {
        while (_isMovingToWaypoints)
        { 
            Move(SetWaypoint());
            yield return null;
        }
    }
}