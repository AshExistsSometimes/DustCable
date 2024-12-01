using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    // Keybinds //

    public static KeyCode JumpKey = KeyCode.Space;
    public static KeyCode CrouchKey = KeyCode.LeftShift;
    public static KeyCode SprintKey = KeyCode.LeftControl;

    public static KeyCode AimKey = KeyCode.Mouse1;
    public static KeyCode FireKey = KeyCode.Mouse0;
    public static KeyCode GrappleKey = KeyCode.Mouse5;

    public static KeyCode InteractKey = KeyCode.F;

    // Input Handling //
    public static Vector3 GetMovementAsVector3()
    {
        return new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    public static Vector2 GetMouseAsVector2()
    {
        return new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
    }

    public static bool GetKey(KeyCode key)
    {
        return Input.GetKey(key);
    }

    public static bool GetKeyDown(KeyCode key)
    {
        return Input.GetKeyDown(key);
    }

    public static bool GetKeyUp(KeyCode key)
    {
        return Input.GetKeyUp(key);
    }

}
