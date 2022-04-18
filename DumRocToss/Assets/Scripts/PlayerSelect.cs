using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    public PlayerData player;
    public GameObject thisCostume;
    private SpriteRenderer ren;
    public GameObject text;

    private void Start()
    {
        ren = (SpriteRenderer) GetComponent(typeof(SpriteRenderer));
    }

    private void OnMouseDown()
    {
        player.costume = thisCostume;
    }

    private void Update()
    {
        if (player.costume == thisCostume)
        {
            ren.color = new Color32(145,127,106,255);
            text.SetActive(false);
        }
        else
        {
            ren.color = Color.white;
            text.SetActive(true);
        }
    }
}
