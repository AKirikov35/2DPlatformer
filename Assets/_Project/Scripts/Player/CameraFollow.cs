using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Vector2 _minPosition;
    [SerializeField] private Vector2 _maxPosition;
    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private Vector3 _offset;

    private void LateUpdate()
    {
        Vector3 desiredPosition = _player.position + _offset;

        desiredPosition.x = Mathf.Clamp(desiredPosition.x, _minPosition.x, _maxPosition.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, _minPosition.y, _maxPosition.y);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
        transform.position = smoothedPosition;
    }
}
