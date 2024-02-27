using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player player;

    /// <summary>
    /// 전투가 시작했는지 확인하는 변수 (true : 진행중, false : 끝남)
    /// </summary>
    public bool isBattle = false;

    /// <summary>
    /// 게임시간
    /// </summary>
    public float timer = 0.0f;

    protected override void OnInitialize()
    {
        player = FindAnyObjectByType<Player>();
        timer = 0.0f;
    }

    void Start()
    {
        isBattle = true;
    }

    void Update()
    {
        if(isBattle)
            timer += Time.deltaTime;
    }

    public void BattleBegin()
    {
        isBattle = true;
    }

    public void BattleEnd()
    {
        isBattle = false;
    }
}
