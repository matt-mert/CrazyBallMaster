using UnityEngine;
using UnityEngine.InputSystem;

public class TestInput : MonoBehaviour
{
    public void TestUserInput(InputAction.CallbackContext ctx)
    {
        Debug.Log(ctx.ReadValue<Vector2>());
    }
}
