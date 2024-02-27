using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 무기 콜라이더를 설정하는 스크립트
/// </summary>
public class WeaponControl : MonoBehaviour
{
    Collider coll;

    bool isEnable = false;
    [SerializeField] bool isDefenced = false;

    void Awake()
    {
        coll = GetComponent<Collider>();
        coll.enabled = false;
    }

    void FixedUpdate()
    {
        if (!coll.enabled) // 콜라이더가 비활성화 되면 조건 비활성화
            isDefenced = false;
    }

    // 방패에 닿았는지 체크
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield"))
        {
            isDefenced = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Shield"))
        {
            isDefenced = false;
        }
    }

    /// <summary>
    /// 무기 콜라이더를 활성화, 비활성화 하는 함수 (실행하면 bool값이 전환됨, 초기값: false)
    /// </summary>
    public void ChangeColliderEnableState()
    {
        isEnable = !isEnable;
        coll.enabled = isEnable;
    }

    //public void ChangeIsDefencedState()
    //{
    //    isDefenced = !isDefenced;
    //}

    /// <summary>
    /// 공격이 방패에 막혔는지 여부를 반환하는 함수 (true : 막힘, false : 안막힘)
    /// </summary>
    /// <returns>방어 당했는지 확인하는 bool값</returns>
    public bool CheckIsDefenced()
    {
        return isDefenced;
    }
}
