using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStateBase : MonoBehaviour
{
    /// <summary>
    /// 적 메인 스크립트
    /// </summary>
    public EnemyBase enemy;

    /// <summary>
    /// 현재 State가 시작 될 때 실행되는 내용
    /// </summary>
    /// <returns></returns>
    public abstract EnemyStateBase EnterCurrentState();

    /// <summary>
    /// 현재 State가 종료 될 때 실행되는 내용
    /// </summary>
    /// <returns></returns>
    public abstract EnemyStateBase ExitCurrentState();

    /// <summary>
    /// 현재 State에서 실행되는 내용 (Update)
    /// </summary>
    /// <returns></returns>
    public abstract EnemyStateBase RunCurrentState();
}