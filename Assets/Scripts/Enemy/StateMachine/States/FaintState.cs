using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Toughness가 0이되면 실행되는 스크립트
/// </summary>
public class FaintState : EnemyStateBase
{
    float timer = 0f;
    float faintTime; // 기절 시간

    bool isFaintEnd = true;

    public override EnemyStateBase EnterCurrentState()
    {
        // 초기화
        float animTime = enemy.GetAnimClipLength("Faint"); // 애니메이션 재생시간
        faintTime = animTime + 0.5f; // 일어나는 시간
        timer = 0f; // 타이머 초기화
        isFaintEnd = true; // 조건 초기화

        enemy.Anim.SetTrigger(enemy.faintToHash); // 기절 애니메이션

        StopCoroutine(FaintAnimCoroutine());
        StartCoroutine(FaintAnimCoroutine());
        return this;
    }

    public override EnemyStateBase ExitCurrentState()
    {
        return this;
    }

    public override EnemyStateBase RunCurrentState()
    {
        if(!isFaintEnd) timer += Time.deltaTime;

        if(timer > faintTime)
        {
            enemy.Toughness = enemy.maxToughness; // 강인성 초기화 후 
            return enemy.SetEnemyState(EnemyBase.State.Chasing); // 상태 변경
        }

        return this;
    }

    IEnumerator FaintAnimCoroutine()
    {
        enemy.Anim.SetBool(enemy.isFaintToHash, isFaintEnd); // 기절 bool 애니메이션
        yield return new WaitForSeconds(1.2f);

        isFaintEnd = false;
        enemy.Anim.SetBool(enemy.isFaintToHash, isFaintEnd); // 기절 bool 애니메이션
    }
}
