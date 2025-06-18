using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private int _groundContactCount = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
            _groundContactCount++;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
            _groundContactCount = Mathf.Max(0, _groundContactCount - 1);
    }

    public bool IsGrounded() => _groundContactCount > 0;
}