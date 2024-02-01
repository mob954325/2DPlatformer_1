using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player_Afterimage : MonoBehaviour
{
    Player player;
    public GameObject[] _playerImages;
    public Transform[] _imagesTransforms;
    public float _frequency;
    [Range(1, 10)]
    public float interval;
    // after Image
    // create 5 image array
    // each image has own position and show in order

    void Awake()
    {
        player = FindAnyObjectByType<Player>();
        InitializeArray();
    }

    void Update()
    {
        transform.position = player.transform.position;
    }

    public IEnumerator CreateAfterImage()
    {
        for (int i = 0; i < _imagesTransforms.Length; i++)
        {
            _playerImages[i].GetComponent<SpriteRenderer>().sprite = player._lastSprite;
            _playerImages[i].GetComponent<SpriteRenderer>().flipX = player._isFlipX;
            

            _playerImages[i].SetActive(true);
            _imagesTransforms[i].position = player._dashSpot + ((player.transform.position - player._dashSpot).normalized * i / interval);
            yield return new WaitForSeconds(_frequency);
        }

        for (int i = 0; i < _imagesTransforms.Length; i++)
        {
            _playerImages[i].SetActive(false);
            yield return new WaitForSeconds(_frequency);
        }

    }

    void InitializeArray()
    {
        _imagesTransforms = new Transform[_playerImages.Length]; // create array

        for(int i = 0; i < _imagesTransforms.Length; i++)
        {
            _imagesTransforms[i] = _playerImages[i].transform;
        }
    }
}
