using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Enemy : Bullet
{
   // bool _isFlip = false;
    public Vector3 _direction;
    public Enemy _enemy;
    SpriteRenderer _sprite;
    Animator _anim;

    //readonly int isCrouch_String = Animator.StringToHash("isCrouch"); 

    void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }
    void Start()
    {
        //_isFlip = _enemy._isFlipX;
    }

    public override void Active()
    {
        transform.position += _direction * _speed * Time.deltaTime;
        //_sprite.flipX = true;
    }

    public override void CompareCollider(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(_hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
