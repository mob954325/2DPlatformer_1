using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    Animator anim;
    float animLength = 0.0f;

    void Awake()
    {
        anim = GetComponent<Animator>();
        animLength = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length; // 애니메이션 길이 추출
    }

    void Start()
    {
        Destroy(this.gameObject, animLength);
    }
}
