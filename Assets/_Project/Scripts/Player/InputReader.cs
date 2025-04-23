using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const KeyCode Jump = KeyCode.Space;
    private const KeyCode Attack = KeyCode.Mouse0;

    private bool _isJump = false;
    private bool _isAttack = false;

    public float MoveDirection { get; private set; }

    private void Update()
    {
        MoveDirection = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(Jump))
            _isJump = true;

        if (Input.GetKeyDown(Attack))
            _isAttack = true;
    }

    public bool GetIsAttack() => GetBoolAsTrigger(ref _isAttack);

    public bool GetIsJump() => GetBoolAsTrigger(ref _isJump);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}