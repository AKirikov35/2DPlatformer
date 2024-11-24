using UnityEngine;

public static class InputReader
{
    public const string Horizontal = nameof(Horizontal);
    public const KeyCode KeyJump = KeyCode.Space;

    public static bool IsMoveInput() => Input.GetAxis(Horizontal) != 0;
}