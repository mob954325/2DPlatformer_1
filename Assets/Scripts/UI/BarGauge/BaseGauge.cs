using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 게이지 UI 베이스 스크립트
/// </summary>
public class BaseGauge : MonoBehaviour
{
    void Awake()
    {
        Init();
    }

    void Update()
    {
        UpdateUI();
    }

    /// <summary>
    /// 스크립트가 실행될 때 가장 먼저 실행될 초기화 함수
    /// </summary>
    protected virtual void Init()
    {

    }

    /// <summary>
    /// Update 함수에 들어갈 UI업데이트 함수
    /// </summary>
    protected virtual void UpdateUI()
    {

    }
}
