using UnityEngine;

public class Rotator : MonoBehaviour
{
    private const float LeftAngle = 180f;
    private const float RightAngle = 0f;

    private float _currentAngle;

    private void Awake()
    {
        _currentAngle = RightAngle;
        transform.rotation = Quaternion.Euler(0f, _currentAngle, 0f);
    }

    public void Rotate(float direction)
    {
        if (direction == 0f)
            return;

        _currentAngle = direction < 0f ? LeftAngle : RightAngle;
        transform.rotation = Quaternion.Euler(0f, _currentAngle, 0f);
    }
}