using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_StateMachine : TestInput
{
    public EnemyBase enemy;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        enemy.HP = 1;
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        enemy.Toughness = 1;
    }
}
