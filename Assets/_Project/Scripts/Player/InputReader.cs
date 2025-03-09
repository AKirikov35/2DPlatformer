using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const KeyCode Jump = KeyCode.Space;

    private bool _isJump = false;

    public float MoveDirection { get; private set; }

    private void Update()
    {
        MoveDirection = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(Jump))
            _isJump = true;
    }

    public bool GetIsJump() => GetBoolAsTrigger(ref _isJump);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}