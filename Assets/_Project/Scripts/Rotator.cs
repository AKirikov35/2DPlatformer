using UnityEngine;

public class Rotator : MonoBehaviour
{
    private Quaternion _left;
    private Quaternion _right;
    private Quaternion _current;

    private void Awake()
    {
        _left = Quaternion.Euler(0f, 180f, 0f);
        _right = Quaternion.Euler(0f, 0f, 0f);
        _current = _right;
        transform.rotation = _current;
    }

    public void Rotate(float direction)
    {
        if (direction == 0f) 
            return;

        _current = direction < 0f ? _left : _right;
        transform.rotation = _current;
    }
}