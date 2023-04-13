using UnityEngine;

public class InputController
{
    private const string VERTICAL = "Vertical";
    private const string HORIZONTAL = "Horizontal";

    public static bool IsJumping()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public static bool IsRunning()
    {
        return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }

    public static float GetLeftRight()
    {
        return Input.GetAxis(HORIZONTAL);
    }

    public static float GetForwardBackward()
    {
        return Input.GetAxis(VERTICAL);
    }
}
