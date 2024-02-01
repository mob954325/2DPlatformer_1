using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet_Player : Bullet
{
    float _durationTime = 2f;

    bool _isFlip = false;
    public Player _player;
    SpriteRenderer _sprite;

    void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();    
    }
    void Start()
    {
        _isFlip = _player._isFlipX;
        Destroy(gameObject, _durationTime);
    }

    public override void Active()
    {
        if (_isFlip)
        {
            transform.position += Vector3.left * _speed * Time.deltaTime;
            _sprite.flipX = true;
        }
        else if (!_isFlip)
        {
            transform.position += Vector3.right * _speed * Time.deltaTime;
        }
    }

    public override void CompareCollider(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")
            || collision.gameObject.CompareTag("Platform"))
        {
            Instantiate(_hitEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
