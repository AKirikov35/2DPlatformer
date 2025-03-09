using UnityEngine;

public class Rotator : MonoBehaviour
{
    private const float LeftAngle = 180f;
    private const float RightAngle = 0f;

    private float currentAngle;

    private void Start()
    {
        currentAngle = RightAngle;
        transform.rotation = Quaternion.Euler(0f, currentAngle, 0f);
    }

    public void Rotate(float direction)
    {
        if (direction == 0f)
            return;

        currentAngle = direction < 0f ? LeftAngle : RightAngle;
        transform.rotation = Quaternion.Euler(0f, currentAngle, 0f);
    }
}
