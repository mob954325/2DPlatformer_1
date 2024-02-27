using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 방패 콜리이더를 설정하는 스크립트
/// </summary>
public class ShieldControl : MonoBehaviour
{
    Collider coll;

    public float timer = 0f; // 패링할 수 있는 콜라이더를 체크하는 타이머

    bool isEnable = false;

    void Awake()
    {
        coll = GetComponent<Collider>();
    }

    void Update()
    {
        if (isEnable)
            timer += Time.deltaTime;
        else
            timer = 0f;

        if (timer > 0.5f)
        {
            coll.enabled = false;
        }
    }

    public void ChangeColliderEnableState()
    {
        isEnable = !isEnable;
        coll.enabled = isEnable;
    }

    public void DisableCollider()
    {
        coll.enabled = false;
    }
}
