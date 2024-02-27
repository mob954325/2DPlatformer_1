using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 카메라 위치 조정하는 클래스
/// </summary>
public class FollowCamera : MonoBehaviour
{
    public Player player;
    public Vector3 Offset;

    void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    void FixedUpdate()
    {
        transform.position = player.gameObject.transform.position + Offset;
    }
}
