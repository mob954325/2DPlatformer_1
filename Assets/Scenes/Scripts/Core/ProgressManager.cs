using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public float _isStart;

    public GameObject _boss;
    [Tooltip("Check Trigger Object, The Game Object name is MUST BE EnterBossRoom")]
    public bool _isMeetBoss;

    [Tooltip("check Obejectname : BossHealth")]
    public GameObject BossHpBar;

    public GameObject VictoryPanel;
    public GameObject GameOverPanel;

    void Awake()
    {
        _boss = GameObject.Find("Boss").gameObject;
        _boss.SetActive(false);
        BossHpBar = GameObject.Find("BossHealth").gameObject;
    }

    public void ShowBossHp()
    {
        _boss.SetActive(true);
        BossHpBar.transform.GetChild(0).gameObject.SetActive(true);
    }
}
