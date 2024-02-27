using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_PlaySetting : TestInput
{
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Cursor.lockState = CursorLockMode.Locked; // 커서 고정
            Cursor.visible = false; // 커서 가리기
        }
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
