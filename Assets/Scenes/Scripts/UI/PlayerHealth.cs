using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject[] _playerHealthObject;
    Player _player;
    int _playerMaxHp;
    int _playerCurrentHp;


    void Awake()
    {
        _player = FindAnyObjectByType<Player>();  
    }

    void OnEnable()
    {
        // init value
        _playerCurrentHp = _player._Hp;
        _playerMaxHp = _player._maxHp;

        _playerHealthObject = new GameObject[_playerMaxHp];
        for(int i = 0; i < _playerMaxHp; i++)
        {
            _playerHealthObject[i] = transform.GetChild(i).transform.GetChild(0).gameObject;
        }
    }

    public void ChangeHealth()
    {
        // check
        _playerCurrentHp = _player._Hp;
        _playerMaxHp = _player._maxHp;

        int _changeHealthNum = _playerMaxHp - _playerCurrentHp;

        Debug.Log($"{_changeHealthNum}");
        for (int i = _playerCurrentHp - 1; i <= 0; i--)
        {
            _playerHealthObject[i].SetActive(true);
        }
        for(int i = 0; i < _changeHealthNum; i++)
        {
            // max - cur = 현재 비활성화할 체력 수
            _playerHealthObject[i].SetActive(false);
        }

    }
}
