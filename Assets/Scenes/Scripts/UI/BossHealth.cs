using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    Scrollbar scrollbar;
    public Enemy_Boss _boss;

    float _Health;

    void Awake()
    {
        scrollbar = GetComponent<Scrollbar>();
        _boss = FindAnyObjectByType<Enemy_Boss>();
    }

    void LateUpdate()
    {
        float _curBossHealth = _boss._Hp;
        scrollbar.size = _curBossHealth / _boss._maxHp;

        if(_curBossHealth <= 0)
        {
            BossDead();
        }
    }

    public void BossDead()
    {
        gameObject.SetActive(false);
    }
}
