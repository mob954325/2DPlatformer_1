using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// EnemyBarObject에 들어갈 스크립트
/// </summary>
public class EnemyBar : BaseGauge
{
    // HSEnemy
    EnemyBase enemy;

    Image currentEnemyHP;
    Image currentEnemyToughness;

    protected override void Init()
    {
        enemy = FindAnyObjectByType<EnemyBase>(); // enemy 스크립트가 있는 오브젝트 찾기
        if(enemy == null)
        {
            // 임시
            enemy = FindAnyObjectByType<EnemyBase>(); // enemy 스크립트가 있는 오브젝트 찾기
            gameObject.SetActive(false); // 숨기기
        }

        Transform hpChild = transform.GetChild(0).GetChild(1);
        Transform toughChild = transform.GetChild(1).GetChild(1);

        currentEnemyHP = hpChild.GetComponent<Image>();
        currentEnemyToughness = toughChild.GetComponent<Image>();
    }

    protected override void UpdateUI()
    {
        if(enemy.HP < 1) // 적이 사망했거나 없으면
        {
            gameObject.SetActive(false); // 숨기기
        }

        currentEnemyHP.fillAmount = enemy.HP / (float)enemy.maxHp;
        currentEnemyToughness.fillAmount = enemy.Toughness / (float)enemy.maxToughness;

    }
}
