using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어를 공격하는 상태
/// </summary>
public class AttackState : EnemyStateBase
{
    public bool isAttack = true;
    bool isBlock = false;

    public override EnemyStateBase EnterCurrentState()
    {
        isBlock = false;
        isAttack = true;
        enemy.speed = 0f;
        enemy.Anim.SetFloat(enemy.SpeedToHash, enemy.speed);// 애니메이션 파라미터 적용

        StopCoroutine(AttackCombo());
        StartCoroutine(AttackCombo()); // 공격 실행

        return this;
    }

    public override EnemyStateBase ExitCurrentState()
    {

        return this;
    }

    public override EnemyStateBase RunCurrentState()
    {
        // 플레이어가 패링을 했으면 피격 애니메이션 실행
        if (enemy.isAttackBlocked && !isBlock)
        {
            isBlock = true;
            OnPlayerParrying();

            if(enemy.Toughness == 0)
                return enemy.SetEnemyState(EnemyBase.State.Faint);
        }

        // 공격이 끝나면 chasing으로 돌아가기
        if (!isAttack)
            return enemy.SetEnemyState(EnemyBase.State.Chasing);

        return this;
    }

    IEnumerator AttackCombo()
    {
        enemy.Anim.SetTrigger(enemy.AttackToHash);
        int randAnimNum = UnityEngine.Random.Range(0, enemy.attackAnimNum); // 랜덤 공격  애니메이션 가져오기 (0 - attackAnimNum 미만 수)

        enemy.Anim.SetInteger(enemy.randomAttackToHash, randAnimNum);

        float animTime = enemy.GetAnimClipLength($"Attack{randAnimNum}");

        yield return new WaitForSeconds(animTime); // 2f / 24.02.25 - 애니메이션 재생시간에 따른 코루틴 대기시간 정하기
        isAttack = false;
    }

    void OnPlayerParrying()
    {
        enemy.Toughness -= 20;
        enemy.Anim.SetTrigger(enemy.DamagedToHash);
        enemy.changeWeaponCollider();
    }
}
