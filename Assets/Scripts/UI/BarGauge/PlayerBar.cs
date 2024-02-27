using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : BaseGauge
{
    Player player;

    Image currentPlayerHP;


    protected override void Init()
    {
        player = FindAnyObjectByType<Player>();
        if(player == null)
        {
            gameObject.SetActive(false);
        }

        Transform child = transform.GetChild(1);
        currentPlayerHP = child.GetComponent<Image>();
    }

    protected override void UpdateUI()
    {
        currentPlayerHP.fillAmount = player.HP / (float)player.maxhp;
    }
}
