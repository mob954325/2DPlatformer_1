using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 정보를 저장하는 클래스
/// </summary>
public class InfoFromObject : MonoBehaviour
{
    public string enemyName; // 이름

    public string Rank; // 등급

    [TextArea]
    public string Desc; // 설명
}
