using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Hp : TestInput
{
    public Player player;
    public HSEnemy enemy;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            enemy.HP -= 1;
        }
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
        }
    }
    protected override void OnTest3(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            enemy.Toughness -= 20;
        }
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Time.timeScale = 0.2f;
        }
    }

    protected override void OnTest5(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Time.timeScale = 1f;
        }
    }
}
