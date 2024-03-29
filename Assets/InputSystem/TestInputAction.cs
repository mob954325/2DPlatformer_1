//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/InputSystem/TestInputAction.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @TestInputAction: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @TestInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""TestInputAction"",
    ""maps"": [
        {
            ""name"": ""Test"",
            ""id"": ""197eb269-a345-44ce-822a-4a429c3e5f95"",
            ""actions"": [
                {
                    ""name"": ""Test_1"",
                    ""type"": ""Button"",
                    ""id"": ""7de7469b-4995-4006-937b-9aae6d34a05d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Test_2"",
                    ""type"": ""Button"",
                    ""id"": ""c32e75b3-33a3-48a9-863e-846d7a78e57a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Test_3"",
                    ""type"": ""Button"",
                    ""id"": ""20342206-df2b-4036-8883-ef012b9f9d18"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Test_4"",
                    ""type"": ""Button"",
                    ""id"": ""9d32566b-4b54-4dee-a9b9-384488c5eab5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Test_5"",
                    ""type"": ""Button"",
                    ""id"": ""d6150bb0-6f61-4b40-8094-6d511e4c2b48"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6dd4e6a7-4321-4bbc-86d1-e75191f6d82f"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Test_1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a5d7730a-0a61-4a72-b03b-20d7c681c958"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Test_2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c58bad9-30e1-49be-bfe5-0ee0d56f8ed3"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Test_3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2a3b128-6194-4e26-95ed-59c56b79f3a8"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Test_4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f492f649-0991-4872-8627-0050728cf2b7"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Test_5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyBoard"",
            ""bindingGroup"": ""KeyBoard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Test
        m_Test = asset.FindActionMap("Test", throwIfNotFound: true);
        m_Test_Test_1 = m_Test.FindAction("Test_1", throwIfNotFound: true);
        m_Test_Test_2 = m_Test.FindAction("Test_2", throwIfNotFound: true);
        m_Test_Test_3 = m_Test.FindAction("Test_3", throwIfNotFound: true);
        m_Test_Test_4 = m_Test.FindAction("Test_4", throwIfNotFound: true);
        m_Test_Test_5 = m_Test.FindAction("Test_5", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Test
    private readonly InputActionMap m_Test;
    private List<ITestActions> m_TestActionsCallbackInterfaces = new List<ITestActions>();
    private readonly InputAction m_Test_Test_1;
    private readonly InputAction m_Test_Test_2;
    private readonly InputAction m_Test_Test_3;
    private readonly InputAction m_Test_Test_4;
    private readonly InputAction m_Test_Test_5;
    public struct TestActions
    {
        private @TestInputAction m_Wrapper;
        public TestActions(@TestInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Test_1 => m_Wrapper.m_Test_Test_1;
        public InputAction @Test_2 => m_Wrapper.m_Test_Test_2;
        public InputAction @Test_3 => m_Wrapper.m_Test_Test_3;
        public InputAction @Test_4 => m_Wrapper.m_Test_Test_4;
        public InputAction @Test_5 => m_Wrapper.m_Test_Test_5;
        public InputActionMap Get() { return m_Wrapper.m_Test; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TestActions set) { return set.Get(); }
        public void AddCallbacks(ITestActions instance)
        {
            if (instance == null || m_Wrapper.m_TestActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_TestActionsCallbackInterfaces.Add(instance);
            @Test_1.started += instance.OnTest_1;
            @Test_1.performed += instance.OnTest_1;
            @Test_1.canceled += instance.OnTest_1;
            @Test_2.started += instance.OnTest_2;
            @Test_2.performed += instance.OnTest_2;
            @Test_2.canceled += instance.OnTest_2;
            @Test_3.started += instance.OnTest_3;
            @Test_3.performed += instance.OnTest_3;
            @Test_3.canceled += instance.OnTest_3;
            @Test_4.started += instance.OnTest_4;
            @Test_4.performed += instance.OnTest_4;
            @Test_4.canceled += instance.OnTest_4;
            @Test_5.started += instance.OnTest_5;
            @Test_5.performed += instance.OnTest_5;
            @Test_5.canceled += instance.OnTest_5;
        }

        private void UnregisterCallbacks(ITestActions instance)
        {
            @Test_1.started -= instance.OnTest_1;
            @Test_1.performed -= instance.OnTest_1;
            @Test_1.canceled -= instance.OnTest_1;
            @Test_2.started -= instance.OnTest_2;
            @Test_2.performed -= instance.OnTest_2;
            @Test_2.canceled -= instance.OnTest_2;
            @Test_3.started -= instance.OnTest_3;
            @Test_3.performed -= instance.OnTest_3;
            @Test_3.canceled -= instance.OnTest_3;
            @Test_4.started -= instance.OnTest_4;
            @Test_4.performed -= instance.OnTest_4;
            @Test_4.canceled -= instance.OnTest_4;
            @Test_5.started -= instance.OnTest_5;
            @Test_5.performed -= instance.OnTest_5;
            @Test_5.canceled -= instance.OnTest_5;
        }

        public void RemoveCallbacks(ITestActions instance)
        {
            if (m_Wrapper.m_TestActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ITestActions instance)
        {
            foreach (var item in m_Wrapper.m_TestActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_TestActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public TestActions @Test => new TestActions(this);
    private int m_KeyBoardSchemeIndex = -1;
    public InputControlScheme KeyBoardScheme
    {
        get
        {
            if (m_KeyBoardSchemeIndex == -1) m_KeyBoardSchemeIndex = asset.FindControlSchemeIndex("KeyBoard");
            return asset.controlSchemes[m_KeyBoardSchemeIndex];
        }
    }
    public interface ITestActions
    {
        void OnTest_1(InputAction.CallbackContext context);
        void OnTest_2(InputAction.CallbackContext context);
        void OnTest_3(InputAction.CallbackContext context);
        void OnTest_4(InputAction.CallbackContext context);
        void OnTest_5(InputAction.CallbackContext context);
    }
}
