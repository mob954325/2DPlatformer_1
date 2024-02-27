using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 게임 시작전 준비 시간
/// </summary>
public class IdleState : EnemyStateBase
{
    [Tooltip("다음 상태(Chasing)로 전환될 때까지 대기 시간")]
    public float changeTime;

    public float timer;

    public override EnemyStateBase EnterCurrentState()
    {
        enemy.speed = 0f;

        return this;
    }

    public override EnemyStateBase RunCurrentState()
    {
        timer += Time.deltaTime;

        if(timer >= changeTime)
        {
            Debug.Log("chasingState로 상태 변경");

            return enemy.SetEnemyState(EnemyBase.State.Chasing);
        }

        return this;
    }

    public override EnemyStateBase ExitCurrentState()
    {
        return this;
    }
}
