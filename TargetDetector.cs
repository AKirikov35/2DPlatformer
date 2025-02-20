using System;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;

    private Vector2 defaultPosition = Vector2.zero;
        
    public SkeletonState SkeletonState { get; private set; }
    public Vector2 TargetPosition { get; private set; }

    private void Awake()
    {
        TargetPosition = defaultPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _))
        { 
            TargetPosition = collision.transform.position; 
            SkeletonState = SkeletonState.Pursue;
        }          
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _))
        {
            Debug.Log(collision.gameObject.name);
            TargetPosition = defaultPosition;
            SkeletonState = SkeletonState.Patrol;
        }
    }
}