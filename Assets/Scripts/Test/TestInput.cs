using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestInput : MonoBehaviour
{
    TestInputAction testAction;

    void Awake()
    {
        testAction = new TestInputAction();
    }

    void OnEnable()
    {
        testAction.Enable();
        testAction.Test.Test_1.performed += OnTest1;
        testAction.Test.Test_1.canceled += OnTest1;
        testAction.Test.Test_2.performed += OnTest2;
        testAction.Test.Test_2.canceled += OnTest2;
        testAction.Test.Test_3.performed += OnTest3;
        testAction.Test.Test_3.canceled += OnTest3;
        testAction.Test.Test_4.performed += OnTest4;
        testAction.Test.Test_4.canceled += OnTest4;
        testAction.Test.Test_5.performed += OnTest5;
        testAction.Test.Test_5.canceled += OnTest5;
    }

    void OnDisable()
    {
        testAction.Test.Test_5.canceled -= OnTest5;
        testAction.Test.Test_5.performed -= OnTest5;
        testAction.Test.Test_4.canceled -= OnTest4;
        testAction.Test.Test_4.performed -= OnTest4;
        testAction.Test.Test_3.canceled -= OnTest3;
        testAction.Test.Test_3.performed -= OnTest3;
        testAction.Test.Test_2.canceled -= OnTest2;
        testAction.Test.Test_2.performed -= OnTest2;
        testAction.Test.Test_1.canceled -= OnTest1;
        testAction.Test.Test_1.performed -= OnTest1;
        testAction.Disable();
    }

    protected virtual void OnTest1(InputAction.CallbackContext context)
    {
        
    }

    protected virtual void OnTest2(InputAction.CallbackContext context)
    {
        
    }

    protected virtual void OnTest3(InputAction.CallbackContext context)
    {
        
    }

    protected virtual void OnTest4(InputAction.CallbackContext context)
    {
        
    }

    protected virtual void OnTest5(InputAction.CallbackContext context)
    {
        
    }
}
