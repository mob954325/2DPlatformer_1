using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : EnemyStateBase
{
    public override EnemyStateBase EnterCurrentState()
    {
        enemy.Anim.SetTrigger(enemy.DieToHash);
        GameUIManager.Instance.ShowResult();
        GameManager.Instance.player.DisablePlayerAction(); // 플레이어 입력 비활성화
        GameManager.Instance.BattleEnd(); // 게임 타이머 정지
        return this;
    }

    public override EnemyStateBase ExitCurrentState()
    {
        return this;
    }

    public override EnemyStateBase RunCurrentState()
    {
        return this;
    }
}
