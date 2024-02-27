using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_UI : TestInput
{
    public GameObject infoUI;
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        infoUI.SetActive(true);
    }
    protected override void OnTest2(InputAction.CallbackContext context)
    {
        infoUI.SetActive(false);
    }
}
