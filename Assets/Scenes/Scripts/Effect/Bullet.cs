using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // must add effect list to override bullets
    public float _speed = 20f;
    public GameObject _hitEffect;
    public virtual void CompareCollider(Collision2D collision) // enter colliderEnter2D script
    {

    }

    public virtual void Active() // enter Flip Sprite script , It'll start at Update
    {

    }

    void Update()
    {
        Active();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        CompareCollider(collision);
    }

}
