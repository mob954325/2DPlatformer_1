using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_EnemyAnimation : TestInput
{
    public Animator animtor;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        animtor.SetTrigger("Play");
    }
}
